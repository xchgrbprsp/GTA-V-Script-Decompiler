using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Decompiler
{
	[Serializable]
	public class DecompilingException : Exception
	{
		public DecompilingException(string Message) : base(Message)
		{
		}
	}

	public class Function
	{
		public string Name { get; set; }
		public int Pcount { get; private set; }
		public int Vcount { get; private set; }
		public int Rcount { get; private set; }
		public int Location { get; private set; }
		public int MaxLocation { get; private set; }

		internal List<HLInstruction> Instructions;

		internal Dictionary<int, int> InstructionMap;

		Stack Stack;

		int Offset = 0;

		public ScriptFile Scriptfile;

		public Types.TypeInfo ReturnType { get; private set; }

		internal bool Decoded { get; private set; }

		internal bool DecodeStarted = false;

		internal bool PreDecoded = false;

		internal bool PreDecodeStarted = false;

		public VariableStorage Vars { get; private set; }
		public VariableStorage Params { get; private set; }

		public int LineCount = 0;

		public Comments comments = new();

		internal MainTree MainTree { get; private set; }

		public uint Hash { get; private set; }

		public Function(ScriptFile Owner, string name, int pcount, int vcount, int rcount, int location, int locmax = -1)
		{
			this.Scriptfile = Owner;
			Name = name;
			Pcount = pcount;
			Vcount = vcount;
			Rcount = rcount;
			Location = location;
			if (locmax != -1)
				MaxLocation = locmax;
			else
				MaxLocation = Location;
			Decoded = false;
			Vars = new VariableStorage(VariableStorage.ListType.Vars, vcount - 2);
			Params = new VariableStorage(VariableStorage.ListType.Params, pcount);
			Stack = new(this);
			MainTree = new(this);
			ReturnType = Types.GetTypeInfo(Stack.DataType.Unk);
		}

        /// <summary>
        /// Gets a persistent hash of the function that should not change across updates
        /// </summary>
        /// <remarks>
		/// DO NOT call this after function decode as things get nopped
        /// </remarks>
        uint GetFunctionHash()
		{
			StringBuilder sb = new();
			sb.Append(Pcount);
			sb.Append(Rcount);

			//if (Instructions.Count <= 5)
			//	return 0; too many collisions but lets still give it a try

			int i = 0;
			HLInstruction? lastIns = null;

			foreach (var ins in Instructions)
			{
				sb.Append(ins.Instruction.ToString());
				i++;

				if (ins.Instruction == Instruction.LOCAL_U8 || ins.Instruction == Instruction.LOCAL_U16 || ins.Instruction == Instruction.LOCAL_U8_LOAD || ins.Instruction == Instruction.LOCAL_U16_LOAD || ins.Instruction == Instruction.LOCAL_U8_STORE || ins.Instruction == Instruction.LOCAL_U16_STORE)
					sb.Append(ins.GetOperandsAsUInt); // TODO

				else if (ins.Instruction == Instruction.PUSH_CONST_U8)
					sb.Append(ins.GetOperand(0));
				else if (ins.Instruction == Instruction.PUSH_CONST_U8_U8)
				{
					sb.Append(ins.GetOperand(0));
					sb.Append(ins.GetOperand(1));
                }
				else if (ins.Instruction == Instruction.PUSH_CONST_U8_U8_U8)
				{
                    sb.Append(ins.GetOperand(0));
                    sb.Append(ins.GetOperand(1));
                    sb.Append(ins.GetOperand(2));
                }
				else if (ins.Instruction == Instruction.NATIVE)
				{
					sb.Append(ins.GetNativeParams);
					sb.Append(ins.GetNativeReturns);
                }
				else if (ins.Instruction == Instruction.STRING)
				{
					if (lastIns != null && (lastIns.Instruction == Instruction.PUSH_CONST_U8 || lastIns.Instruction == Instruction.PUSH_CONST_U24 || lastIns.Instruction == Instruction.PUSH_CONST_U32))
						sb.Append(Scriptfile.StringTable[lastIns.GetOperandsAsInt]);
				}

                if (i > 22)
					break;

				lastIns = ins;
			}

			return Utils.Joaat(sb.ToString());
        }

		internal void HintReturnType(Stack.DataType type)
		{
			var ti = Types.GetTypeInfo(type);
			if (ti > ReturnType)
				ReturnType = ti;
		}

		/// <summary>
		/// Disposes of the function and returns the function text
		/// </summary>
		/// <returns>The whole function high level code</returns>
		public override string ToString()
		{
			string str = FunctionHeader() + Environment.NewLine + MainTree.ToString();
			LineCount = Regex.Matches(str, Environment.NewLine).Count + 1;
			return str;
        }

		/// <summary>
		/// Gets the first line of the function Declaration
		/// return type + name + params
		/// </summary>
		public string FunctionHeader()
		{
			StringBuilder working = new();

			//extract return type of function
			foreach (var comment in comments)
			{
				working.AppendLine("// " + comment);
			}

			if (Rcount == 0)
				working.Append("void ");
			else if (Rcount == 1)
			{
				working.Append(ReturnType.ReturnType);
			}

			else if (Rcount == 3)
				working.Append("Vector3 ");

			else if (Rcount > 1)
			{
				if (ReturnType.Type == Stack.DataType.String)
				{
					working.Append("char[" +(Rcount * 4).ToString() + "] ");
				}
				else
				{
					working.Append("struct<" + Rcount.ToString() + "> ");
				}
			}
			else 
				throw new DecompilingException("Unexpected return count");

			working.Append(Name);
			working.Append("(" + Params.GetPDec() + ")");
			if (Properties.Settings.Default.IncludeFunctionPosition || Properties.Settings.Default.IncludeFunctionHash)
				working.Append(" //");

			if (Properties.Settings.Default.IncludeFunctionPosition)
				working.Append(" Position - 0x" + Location.ToString("X"));

            if (Properties.Settings.Default.IncludeFunctionHash)
                working.Append(" Hash - 0x" + Hash.ToString("X"));

            return working.ToString();
		}

		/// <summary>
		/// Determines if a frame variable is a parameter or a variable and returns its name
		/// </summary>
		/// <param name="index">the frame variable index</param>
		/// <returns>the name of the variable</returns>
		public string GetFrameVarName(uint index)
		{
			if (index < Pcount)
				return Params.GetVarName(index);
			else if (index < Pcount + 2)
				throw new Exception("Unexpected fvar");
			return Vars.GetVarName((uint) (index - 2 - Pcount));
		}

		/// <summary>
		/// Determines if a frame variable is a parameter or a variable and returns its index
		/// </summary>
		/// <param name="index">the frame variable index</param>
		/// <returns>The variable</returns>
		public Variable GetFrameVar(uint index)
		{
			if (index < Pcount)
				return Params.GetVarAtIndex(index);
			else if (index < Pcount + 2)
				throw new Exception("Unexpected fvar");
			return Vars.GetVarAtIndex((uint) (index - 2 - Pcount));
		}

        public void SetFrameVarAutoName(uint index, string name)
        {
			VariableStorage storage;
			uint idx;

			if (index < Pcount)
			{
				idx = index;
				storage = Params;
			}
			else if (index < Pcount + 2)
			{
                throw new Exception("Unexpected fvar");
			}
			else
			{
				storage = Vars;
                idx = (uint)(index - 2 - Pcount);
            }

            foreach (var var in storage.Vars)
            {
                if (var.Index != idx && var.Name == name)
                {
                    int num;

                    if (Int32.TryParse("" + name[^1], out num))
						name = name[0..^1] + (num + 1);
					else
						name += "2";
                    break;
                }
            }

            storage.GetVarAtIndex(idx).Name = name;
        }

		public void SetFrameVarAutoLoopIdx(uint index)
		{
            VariableStorage storage;
            uint idx;

            if (index < Pcount)
            {
                idx = index;
                storage = Params;
            }
            else if (index < Pcount + 2)
            {
                throw new Exception("Unexpected fvar");
            }
            else
            {
                storage = Vars;
                idx = (uint)(index - 2 - Pcount);
            }

			for (char i = 'i'; i < 'o'; i++)
			{
				foreach (var var in storage.Vars)
				{
					if (var.Index != idx && var.Name == i.ToString())
					{
						goto skip;
					}
				}

                storage.GetVarAtIndex(idx).Name = i.ToString();
                break;

skip:
				continue;
			}
		}

        /// <summary>
        /// The block of code that the function takes up
        /// </summary>
        public List<byte> CodeBlock { get; set; }

		/// <summary>
		/// Gets the function info given the offset where its called from
		/// </summary>
		/// <param name="offset">the offset that is being called</param>
		/// <returns>basic information about the function at that offset</returns>
		public Function GetFunctionFromOffset(int offset)
		{
			foreach (Function f in Scriptfile.Functions)
			{
				if (f.Location <= offset && offset <= f.MaxLocation)
					return f;
			}
			throw new Exception("Function Not Found");
		}

		/// <summary>
		/// The method that actually decodes the function into high level
		/// </summary>
		public void Decompile()
		{
			if (DecodeStarted)
				return;

			lock (Program.ThreadLock)
			{
				DecodeStarted = true;
				if (Decoded) 
					return;
			}

			DecodeStatementTree(MainTree);
			Decoded = true;
		}

		/// <summary>
		/// Check if a jump is jumping out of the function
		/// if not, then add it to the list of instructions
		/// </summary>
		void IsJumpWithinFunctionBounds()
		{
			int cur = Offset;
			HLInstruction temp = new HLInstruction(CodeBlock[Offset], GetArray(2), cur);
			if (temp.GetJumpOffset > 0)
			{
				if (temp.GetJumpOffset < CodeBlock.Count)
				{
					AddInstruction(cur, temp);
					return;
				}
			}

			//if the jump is out the function then its useless
			//So nop this jump
			AddInstruction(cur, new HLInstruction((byte) 0, cur));
			AddInstruction(cur + 1, new HLInstruction((byte) 0, cur + 1));
			AddInstruction(cur + 2, new HLInstruction((byte) 0, cur + 2));
		}

		/// <summary>
		/// See if a dup is being used for an AND or OR
		/// if it is, dont add it (Rockstars way of doing and/or conditionals)
		/// </summary>
		/// <remarks>Do we really need this?</remarks>
		void CheckDupForInstruction()
		{
			//May need refining, but works fine for rockstars code
			int off = 0;
			Start:
			off += 1;
			if (CodeBlock[Offset + off] == 0)
				goto Start;
			if (CodeBlock[Offset + off] == 86)
			{
				Offset = Offset + off + 2;
				return;
			}
			if (CodeBlock[Offset + off] == 6)
			{
				goto Start;
			}
			Instructions.Add(new HLInstruction(CodeBlock[Offset], Offset));
			return;
		}

		/// <summary>
		/// Gets the given amount of bytes from the codeblock at its offset
		/// while advancing its position by how ever many items it uses
		/// </summary>
		/// <param name="items">how many bytes to grab</param>
		/// <returns>the operands for the instruction</returns>
		IEnumerable<byte> GetArray(int items)
		{
			int temp = Offset + 1;
			Offset += items;

			return CodeBlock.GetRange(temp, items);
		}

		/// <summary>
		/// Create a switch statement, then set up the rest of the decompiler to handle the rest of the switch statement
		/// </summary>
		void HandleSwitch(StatementTree tree)
		{
			Dictionary<int, List<Ast.AstToken>> cases = new Dictionary<int, List<Ast.AstToken>>();
			Ast.AstToken case_val;
			int offset;
			int defaultloc;
			int breakloc;
			bool usedefault;
			HLInstruction temp;

			for (int i = 0; i < Instructions[tree.Offset].GetOperand(0); i++)
			{
				//Check if the case is a known hash
				case_val = new Ast.ConstantInt(this, (ulong)Instructions[tree.Offset].GetSwitchCase(i));

				//Get the offset to jump to
				offset = Instructions[tree.Offset].GetSwitchOffset(i);


				if (!cases.ContainsKey(offset))
				{
					//unknown offset
					cases.Add(offset, new List<Ast.AstToken>(new Ast.AstToken[] {case_val}));
				}
				else
				{
					//This offset is known, multiple cases are jumping to this path
					cases[offset].Add(case_val);
				}
			}

			//Not sure how necessary this step is, but just incase R* compiler doesnt order jump offsets, do it anyway
			List<int> sorted = cases.Keys.ToList();
			sorted.Sort();

			//Hanldle(skip past) any Nops immediately after switch statement
			int tempoff = 0;
			while (Instructions[tree.Offset + 1 + tempoff].Instruction == Instruction.NOP)
				tempoff++;

			//Extract the location to jump to if no cases match
			defaultloc = Instructions[tree.Offset + 1 + tempoff].GetJumpOffset;

			//We have found the jump location, so that instruction is no longer needed and can be nopped
			Instructions[tree.Offset + 1 + tempoff].NopInstruction(); // TODO nopping stuff seems strange

			//Temporary stage
			breakloc = defaultloc;
			usedefault = true;

			//check if case last instruction is a jump to default location, if so default location is a break;
			//if not break location is where last instrcution jumps to
			for (int i = 0; i <= sorted.Count; i++)
			{
				int index = 0;
				if (i == sorted.Count)
					index = InstructionMap[defaultloc] - 1;
				else
					index = InstructionMap[sorted[i]] - 1;
				if (index - 1 == tree.Offset)
				{
					continue;
				}
				temp = Instructions[index];
				if (temp.Instruction != Instruction.J)
				{
					continue;
				}
				if (temp.GetJumpOffset == defaultloc)
				{
					usedefault = false;
					breakloc = defaultloc;
					break;
				}
				breakloc = temp.GetJumpOffset;
			}
			if (usedefault)
			{
				//this seems to be causing some errors
				// if (allreturns)
				// usedefault = false;
			}

			if (usedefault)
			{
				//Default location found, best add it in
				if (cases.ContainsKey(defaultloc))
				{
					//Default location shares code path with other known case
					cases[defaultloc].Add(new Ast.Default(this));
				}
				else
				{
                    //Default location is a new code path
                    cases.Add(defaultloc, new List<Ast.AstToken>(new Ast.AstToken[] { new Ast.Default(this) }));
                    sorted = cases.Keys.ToList();
					sorted.Sort();
				}
			}

			var @switch = new SwitchTree(this, tree, tree.Offset, cases, breakloc, Stack.Pop());

			foreach (var statement in @switch.Statements)
			{
				DecodeStatementTree(statement as StatementTree);
			}

			tree.Statements.Add(@switch);
			tree.Offset = CodeOffsetToFunctionOffset(breakloc) - 1;
		}

		/// <summary>
		/// Turns the raw code into a list of instructions
		/// </summary>
		public void BuildInstructions()
		{
			Offset = CodeBlock[4] + 5;
			Instructions = new List<HLInstruction>();
			InstructionMap = new Dictionary<int, int>();
			int curoff;
			while (Offset < CodeBlock.Count)
			{
				while (Offset < CodeBlock.Count)
				{
					curoff = Offset;
					switch (CodeBlock[Offset])
					{
						//		case 0: if (addnop) AddInstruction(curoff, new HLInstruction((byte)0, curoff)); break;
						case 37:
							AddInstruction(curoff, new HLInstruction(CodeBlock[Offset], GetArray(1), curoff));
							break;
						case 38:
							AddInstruction(curoff, new HLInstruction(CodeBlock[Offset], GetArray(2), curoff));
							break;
						case 39:
							AddInstruction(curoff, new HLInstruction(CodeBlock[Offset], GetArray(3), curoff));
							break;
						case 40:
						case 41:
							AddInstruction(curoff, new HLInstruction(CodeBlock[Offset], GetArray(4), curoff));
							break;
						case 42: //Because of how rockstar codes and/or conditionals, its neater to detect dups
							//and only add them if they are not used for conditionals
							CheckDupForInstruction();
							break;
						case 44:
							AddInstruction(curoff, new HLInstruction(CodeBlock[Offset], GetArray(3), curoff));
							break;
						case 45:
							throw new Exception("Function not exptected");
						case 46:
							AddInstruction(curoff, new HLInstruction(CodeBlock[Offset], GetArray(2), curoff));
							break;
						case 52:
						case 53:
						case 54:
						case 55:
						case 56:
						case 57:
						case 58:
						case 59:
						case 60:
						case 61:
						case 62:
						case 64:
						case 65:
						case 66:
							AddInstruction(curoff, new HLInstruction(CodeBlock[Offset], GetArray(1), curoff));
							break;
						case 67:
						case 68:
						case 69:
						case 70:
						case 71:
						case 72:
						case 73:
						case 74:
						case 75:
						case 76:
						case 77:
						case 78:
						case 79:
						case 80:
						case 81:
						case 82:
						case 83:
						case 84:
							AddInstruction(curoff, new HLInstruction(CodeBlock[Offset], GetArray(2), curoff));
							break;
						case 85:
							IsJumpWithinFunctionBounds();
							break;
						case 86:
						case 87:
						case 88:
						case 89:
						case 90:
						case 91:
						case 92:
							AddInstruction(curoff, new HLInstruction(CodeBlock[Offset], GetArray(2), curoff));
							break;
						case 93:
						case 94:
						case 95:
						case 96:
						case 97:
							AddInstruction(curoff, new HLInstruction(CodeBlock[Offset], GetArray(3), curoff));
							break;
						case 98:
							int temp = CodeBlock[Offset + 1];
							AddInstruction(curoff, new HLInstruction(CodeBlock[Offset], GetArray(temp*6 + 1), curoff));
							break;
						case 101:
						case 102:
						case 103:
						case 104:
							AddInstruction(curoff, new HLInstruction(CodeBlock[Offset], GetArray(1), curoff));
							break;
						case 127:
							AddInstruction(curoff, new HLInstruction(CodeBlock[Offset], curoff));
							break;
						default:
							if (CodeBlock[Offset] <= 126) AddInstruction(curoff, new HLInstruction(CodeBlock[Offset], curoff));
							else throw new Exception("Unexpected Opcode");
							break;
					}
					Offset++;
				}
			}

			Hash = GetFunctionHash();
		}


		/// <summary>
		/// Adds an instruction to the list of instructions
		/// then adds the offset to a dictionary
		/// </summary>
		/// <param name="offset">the offset in the code</param>
		/// <param name="instruction">the instruction</param>
		void AddInstruction(int offset, HLInstruction instruction)
		{
			Instructions.Add(instruction);
			InstructionMap.Add(offset, Instructions.Count - 1);
		}

		internal int CodeOffsetToFunctionOffset(int codeOffset)
		{
			return InstructionMap[codeOffset];
		}

		internal int FunctionOffsetToCodeOffset(int funcOffset)
		{
			return (from p in InstructionMap where p.Value == funcOffset select p.Key).First();
		}

		/// <summary>
		/// Decodes the instruction at the current offset
		/// </summary>
		internal const Instruction CONDITIONAL_JUMP = (Instruction)999;
		internal void DecodeStatementTree(StatementTree tree)
		{
			Stack<StatementTree> treeStack = new Stack<StatementTree>();
			treeStack.Push(tree);

			while (true)
			{
				switch (Instructions[tree.Offset].Instruction)
				{
					case Instruction.NOP:
						break;
					case Instruction.IADD:
						Stack.Push(new Ast.IntegerAdd(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.ISUB:
						Stack.Push(new Ast.IntegerSub(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.IMUL:
						Stack.Push(new Ast.IntegerMul(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.IDIV:
						Stack.Push(new Ast.IntegerDiv(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.IMOD:
						Stack.Push(new Ast.IntegerMod(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.INOT:
						Stack.Push(new Ast.IntegerNot(this, Stack.Pop()));
						break;
					case Instruction.INEG:
						Stack.Push(new Ast.IntegerNeg(this, Stack.Pop()));
						break;
					case Instruction.IEQ:
						Stack.Push(new Ast.IntegerEq(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.INE:
						Stack.Push(new Ast.IntegerNe(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.IGT:
						Stack.Push(new Ast.IntegerGt(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.IGE:
						Stack.Push(new Ast.IntegerGe(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.ILT:
						Stack.Push(new Ast.IntegerLt(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.ILE:
						Stack.Push(new Ast.IntegerLe(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.FADD:
						Stack.Push(new Ast.FloatAdd(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.FSUB:
						Stack.Push(new Ast.FloatSub(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.FMUL:
						Stack.Push(new Ast.FloatMul(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.FDIV:
						Stack.Push(new Ast.FloatDiv(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.FMOD:
						Stack.Push(new Ast.FloatMod(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.FNEG:
						Stack.Push(new Ast.FloatNeg(this, Stack.Pop()));
						break;
					case Instruction.FEQ:
						Stack.Push(new Ast.FloatEq(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.FNE:
						Stack.Push(new Ast.FloatNe(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.FGT:
						Stack.Push(new Ast.FloatGt(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.FGE:
						Stack.Push(new Ast.FloatGe(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.FLT:
						Stack.Push(new Ast.FloatLt(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.FLE:
						Stack.Push(new Ast.FloatLe(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.VADD:
						Stack.Push(new Ast.VectorAdd(this, Stack.PopVector(), Stack.PopVector()));
						break;
					case Instruction.VSUB:
						Stack.Push(new Ast.VectorSub(this, Stack.PopVector(), Stack.PopVector()));
						break;
					case Instruction.VMUL:
						Stack.Push(new Ast.VectorMul(this, Stack.PopVector(), Stack.PopVector()));
						break;
					case Instruction.VDIV:
						Stack.Push(new Ast.VectorDiv(this, Stack.PopVector(), Stack.PopVector()));
						break;
					case Instruction.VNEG:
						Stack.Push(new Ast.VectorNeg(this, Stack.PopVector()));
						break;
					case Instruction.IAND:
						Stack.Push(new Ast.IntegerAnd(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.IOR:
						Stack.Push(new Ast.IntegerOr(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.IXOR:
						Stack.Push(new Ast.IntegerXor(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.I2F:
						Stack.Push(new Ast.IntToFloat(this, Stack.Pop()));
						break;
					case Instruction.F2I:
						Stack.Push(new Ast.FloatToInt(this, Stack.Pop()));
						break;
					case Instruction.F2V:
						var val = Stack.Pop();
						if (val.HasSideEffects())
							Stack.Push(new Ast.FloatToVec(this, val));
						else
							Stack.Push(new Ast.Vector(this, val, val, val));
						break;
					case Instruction.PUSH_CONST_U8:
						Stack.Push(new Ast.ConstantInt(this, Instructions[tree.Offset].GetOperand(0)));
						break;
					case Instruction.PUSH_CONST_U8_U8:
						Stack.Push(new Ast.ConstantInt(this, Instructions[tree.Offset].GetOperand(0)));
						Stack.Push(new Ast.ConstantInt(this, Instructions[tree.Offset].GetOperand(1)));
						break;
					case Instruction.PUSH_CONST_U8_U8_U8:
						Stack.Push(new Ast.ConstantInt(this, Instructions[tree.Offset].GetOperand(0)));
						Stack.Push(new Ast.ConstantInt(this, Instructions[tree.Offset].GetOperand(1)));
						Stack.Push(new Ast.ConstantInt(this, Instructions[tree.Offset].GetOperand(2)));
						break;
					case Instruction.PUSH_CONST_U32:
					case Instruction.PUSH_CONST_S16:
					case Instruction.PUSH_CONST_U24:
						Stack.Push(new Ast.ConstantInt(this, (ulong)Instructions[tree.Offset].GetOperandsAsInt));
						break;
					case Instruction.PUSH_CONST_F:
						Stack.Push(new Ast.ConstantFloat(this, Instructions[tree.Offset].GetFloat));
						break;
					case Instruction.DUP:
						var _val = Stack.Pop();
						if (_val.HasSideEffects())
							throw new InvalidOperationException("Cannot dup token with side effects");
						Stack.Push(_val);
						Stack.Push(_val);
						break;
					case Instruction.DROP:
						if (Stack.Peek().GetStackCount() > 1)
						{
							if (Stack.Peek() is Ast.FunctionCallBase)
							{
								(Stack.Peek() as Ast.FunctionCallBase).DropReturn();
								break;
							}
							else
							{
								comments.Add(new("DROP: Dropped token with GetStackCount() > 1 && Peek() is not Ast.FunctionCallBase, not sure what to do, defaulting to incorrect behavior"));
							}
						}
						var dropped = Stack.Pop(true);
						if (dropped.HasSideEffects())
							tree.Statements.Add(new Ast.Drop(this, dropped));
						break;
					case Instruction.NATIVE:
						var native = new Ast.NativeCall(this, Stack.PopCount(Instructions[tree.Offset].GetNativeParams), this.Scriptfile.X64NativeTable.GetNativeFromIndex(Instructions[tree.Offset].GetNativeIndex),
							this.Scriptfile.X64NativeTable.GetNativeHashFromIndex(Instructions[tree.Offset].GetNativeIndex), Instructions[tree.Offset].GetNativeReturns);
						if (native.IsStatement())
							tree.Statements.Add(native);
						else
							Stack.Push(native);
						break;
					case Instruction.ENTER:
						throw new InvalidOperationException("Should not find an ENTER here");
					case Instruction.LEAVE:
						tree.Statements.Add(new Ast.Return(this, Stack.PopCount(Instructions[tree.Offset].GetOperand(1))));
						break;
					case Instruction.LOAD:
						Stack.Push(new Ast.Load(this, Stack.Pop()));
						break;
					case Instruction.STORE:
						tree.Statements.Add(new Ast.Store(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.STORE_REV:
						var value = Stack.Pop();
						tree.Statements.Add(new Ast.Store(this, Stack.Peek(), value));
						break;
					case Instruction.LOAD_N:
						Stack.Push(new Ast.LoadN(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.STORE_N:
						var pointer = Stack.Pop();
						var count = Stack.Pop();
						if (count is not Ast.ConstantInt)
							throw new InvalidOperationException("Cannot handle non-constant STORE_N count");
						tree.Statements.Add(new Ast.StoreN(this, pointer, count, Stack.PopCount((int)(count as Ast.ConstantInt).GetValue())));
						break;
					case Instruction.ARRAY_U8:
					case Instruction.ARRAY_U16:
						Stack.Push(new Ast.Array(this, Instructions[tree.Offset].GetOperandsAsUInt, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.ARRAY_U8_LOAD:
					case Instruction.ARRAY_U16_LOAD:
						Stack.Push(new Ast.ArrayLoad(this, Instructions[tree.Offset].GetOperandsAsUInt, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.ARRAY_U8_STORE:
					case Instruction.ARRAY_U16_STORE:
						tree.Statements.Add(new Ast.ArrayStore(this, Instructions[tree.Offset].GetOperandsAsUInt, Stack.Pop(), Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.LOCAL_U8:
					case Instruction.LOCAL_U16:
						Stack.Push(new Ast.Local(this, Instructions[tree.Offset].GetOperandsAsUInt));
						break;
					case Instruction.LOCAL_U8_LOAD:
					case Instruction.LOCAL_U16_LOAD:
						Stack.Push(new Ast.LocalLoad(this, Instructions[tree.Offset].GetOperandsAsUInt));
						break;
					case Instruction.LOCAL_U8_STORE:
					case Instruction.LOCAL_U16_STORE:
                        tree.Statements.Add(new Ast.LocalStore(this, Instructions[tree.Offset].GetOperandsAsUInt, Stack.Pop()));
						break;
					case Instruction.STATIC_U8:
					case Instruction.STATIC_U16:
						Stack.Push(new Ast.Static(this, Instructions[tree.Offset].GetOperandsAsUInt));
						break;
					case Instruction.STATIC_U8_LOAD:
					case Instruction.STATIC_U16_LOAD:
						Stack.Push(new Ast.StaticLoad(this, Instructions[tree.Offset].GetOperandsAsUInt));
						break;
					case Instruction.STATIC_U8_STORE:
					case Instruction.STATIC_U16_STORE:
						tree.Statements.Add(new Ast.StaticStore(this, Instructions[tree.Offset].GetOperandsAsUInt, Stack.Pop()));
						break;
					case Instruction.IADD_U8:
					case Instruction.IADD_S16:
						Stack.Push(new Ast.IntegerAdd(this, new Ast.ConstantInt(this, (ulong)Instructions[tree.Offset].GetOperandsAsUInt), Stack.Pop()));
						break;
					case Instruction.IMUL_U8:
					case Instruction.IMUL_S16:
						Stack.Push(new Ast.IntegerMul(this, new Ast.ConstantInt(this, (ulong)Instructions[tree.Offset].GetOperandsAsUInt), Stack.Pop())); ;
						break;
					case Instruction.IOFFSET:
						var offset = Stack.Pop();
						Stack.Push(new Ast.Offset(this, Stack.Pop(), offset));
						break;
					case Instruction.IOFFSET_U8:
					case Instruction.IOFFSET_S16:
						Stack.Push(new Ast.Offset(this, Stack.Pop(), new Ast.ConstantInt(this, (ulong)Instructions[tree.Offset].GetOperandsAsUInt)));
						break;
					case Instruction.IOFFSET_U8_LOAD:
					case Instruction.IOFFSET_S16_LOAD:
						Stack.Push(new Ast.OffsetLoad(this, Stack.Pop(), (int)Instructions[tree.Offset].GetOperandsAsUInt));
						break;
					case Instruction.IOFFSET_U8_STORE:
					case Instruction.IOFFSET_S16_STORE:
						tree.Statements.Add(new Ast.OffsetStore(this, Stack.Pop(), (int)Instructions[tree.Offset].GetOperandsAsUInt, Stack.Pop()));
						break;
					case Instruction.GLOBAL_U16:
					case Instruction.GLOBAL_U24:
						Stack.Push(new Ast.Global(this, Instructions[tree.Offset].GetOperandsAsUInt));
						break;
					case Instruction.GLOBAL_U16_LOAD:
					case Instruction.GLOBAL_U24_LOAD:
						Stack.Push(new Ast.GlobalLoad(this, Instructions[tree.Offset].GetOperandsAsUInt));
						break;
					case Instruction.GLOBAL_U16_STORE:
					case Instruction.GLOBAL_U24_STORE:
						tree.Statements.Add(new Ast.GlobalStore(this, Instructions[tree.Offset].GetOperandsAsUInt, Stack.Pop()));
						break;
					case Instruction.CALL:
						foreach (var function in Scriptfile.Functions)
						{
							if (function.Location == Instructions[tree.Offset].GetOperandsAsUInt)
							{
								function.Decompile(); // this is a very bad idea that will break everything but cam give better type inference??? TODO: find better way to propagate type info
								var call = new Ast.FunctionCall(this, Stack.PopCount(function.Pcount), function);

								if (call.IsStatement())
									tree.Statements.Add(call);
								else
									Stack.Push(call);

								goto FUNCTION_FOUND;
							}
						}
						throw new InvalidOperationException("Cannot find function");
FUNCTION_FOUND:
						break;
					case Instruction.STRING:
						Stack.Push(new Ast.String(this, Stack.Pop()));
						break;
					case Instruction.STRINGHASH:
						Stack.Push(new Ast.StringHash(this, Stack.Pop()));
						break;
					case Instruction.TEXT_LABEL_ASSIGN_STRING:
						tree.Statements.Add(new Ast.TextLabelAssignString(this, Stack.Pop(), Stack.Pop(), (int)Instructions[tree.Offset].GetOperandsAsUInt));
						break;
					case Instruction.TEXT_LABEL_ASSIGN_INT:
						tree.Statements.Add(new Ast.TextLabelAssignInt(this, Stack.Pop(), Stack.Pop(), (int)Instructions[tree.Offset].GetOperandsAsUInt));
						break;
					case Instruction.TEXT_LABEL_APPEND_STRING:
						tree.Statements.Add(new Ast.TextLabelAppendString(this, Stack.Pop(), Stack.Pop(), (int)Instructions[tree.Offset].GetOperandsAsUInt));
						break;
					case Instruction.TEXT_LABEL_APPEND_INT:
						tree.Statements.Add(new Ast.TextLabelAppendInt(this, Stack.Pop(), Stack.Pop(), (int)Instructions[tree.Offset].GetOperandsAsUInt));
						break;
					case Instruction.TEXT_LABEL_COPY:
						var ptr = Stack.Pop();
						var _value = Stack.Pop();
						var _count = Stack.Pop();

						if (_count is not Ast.ConstantInt)
							throw new InvalidOperationException("Cannot handle non-constant TEXT_LABEL_COPY count");

						tree.Statements.Add(new Ast.TextLabelCopy(this, ptr, Stack.PopCount((int)(_count as Ast.ConstantInt).GetValue()), _value));
						break;
					case Instruction.CATCH:
					case Instruction.THROW:
						throw new NotImplementedException();
					case Instruction.CALLINDIRECT:
						var location = Stack.Pop();
						tree.Statements.Add(new Ast.IndirectCall(this, Stack.PopCount(Stack.GetCount()), location));
						break;
					case Instruction.PUSH_CONST_M1:
					case Instruction.PUSH_CONST_0:
					case Instruction.PUSH_CONST_1:
					case Instruction.PUSH_CONST_2:
					case Instruction.PUSH_CONST_3:
					case Instruction.PUSH_CONST_4:
					case Instruction.PUSH_CONST_5:
					case Instruction.PUSH_CONST_6:
					case Instruction.PUSH_CONST_7:
						Stack.Push(new Ast.ConstantInt(this, (ulong)Instructions[tree.Offset].GetImmBytePush));
						break;
					case Instruction.PUSH_CONST_FM1:
					case Instruction.PUSH_CONST_F0:
					case Instruction.PUSH_CONST_F1:
					case Instruction.PUSH_CONST_F2:
					case Instruction.PUSH_CONST_F3:
					case Instruction.PUSH_CONST_F4:
					case Instruction.PUSH_CONST_F5:
					case Instruction.PUSH_CONST_F6:
					case Instruction.PUSH_CONST_F7:
						Stack.Push(new Ast.ConstantFloat(this, Instructions[tree.Offset].GetImmFloatPush));
						break;
					case Instruction.IS_BIT_SET:
						Stack.Push(new Ast.BitTest(this, Stack.Pop(), Stack.Pop()));
						break;
					case Instruction.SWITCH:
						HandleSwitch(tree);
						break;
					case Instruction.J:
						var t = tree;

						while (t != null)
						{
							// break from switch case
							if (t is CaseTree)
							{
								if (Instructions[tree.Offset].GetJumpOffset == (t as CaseTree).BreakOffset)
								{
									tree.Statements.Add(new Ast.Break(this));
									goto DONE;
								}
							}

							// break from while loop
                            if (t is WhileTree)
                            {
                                if (Instructions[tree.Offset].GetJumpOffset == (t as WhileTree).BreakOffset)
                                {
                                    tree.Statements.Add(new Ast.Break(this));
                                    goto DONE;
                                }
                            }

                            t = t.Parent;
						}

						// else
                        if (tree is IfTree && Instructions[tree.Offset + 1].Offset == (tree as IfTree).EndOffset && Instructions[tree.Offset].GetJumpOffset != Instructions[tree.Offset + 1].Offset)
						{
							var @else = new ElseTree(this, tree, tree.Offset + 1, Instructions[tree.Offset].GetJumpOffset); // TODO should the parent be tree or tree.Parent?
                            (tree as IfTree).ElseTree = @else;
                            treeStack.Push(@else);
							tree.Parent.Offset = CodeOffsetToFunctionOffset(@else.EndOffset);
							break;
						}

                        int tempoff = 0;
start:
						//Check to see if the jump is just jumping past nops(end of code table)
						//should be the only case for finding another jump now
                        if (Instructions[tree.Offset].GetJumpOffset != Instructions[tree.Offset + 1 + tempoff].Offset)
						{
							if (Instructions[tree.Offset + 1 + tempoff].Instruction == Instruction.NOP)
							{
								tempoff++;
								goto start;
							}
							else if (Instructions[tree.Offset + 1 + tempoff].Instruction == Instruction.J)
							{
								if (Instructions[tree.Offset + 1 + tempoff].GetOperandsAsInt == 0)
								{
									tempoff++;
									goto start;
								}

							}

                            tree.Statements.Add(new Ast.Jump(this, Instructions[tree.Offset].GetJumpOffset));
							break;
                        }
DONE:
                        break;
					case Instruction.JZ:
						goto case CONDITIONAL_JUMP;
					case Instruction.IEQ_JZ:
                        Stack.Push(new Ast.IntegerEq(this, Stack.Pop(), Stack.Pop()));
						goto case CONDITIONAL_JUMP;
                    case Instruction.INE_JZ:
                        Stack.Push(new Ast.IntegerNe(this, Stack.Pop(), Stack.Pop()));
                        goto case CONDITIONAL_JUMP;
                    case Instruction.IGT_JZ:
                        Stack.Push(new Ast.IntegerGt(this, Stack.Pop(), Stack.Pop()));
                        goto case CONDITIONAL_JUMP;
                    case Instruction.IGE_JZ:
                        Stack.Push(new Ast.IntegerGe(this, Stack.Pop(), Stack.Pop()));
                        goto case CONDITIONAL_JUMP;
                    case Instruction.ILT_JZ:
                        Stack.Push(new Ast.IntegerLt(this, Stack.Pop(), Stack.Pop()));
                        goto case CONDITIONAL_JUMP;
                    case Instruction.ILE_JZ:
                        Stack.Push(new Ast.IntegerLe(this, Stack.Pop(), Stack.Pop()));
                        goto case CONDITIONAL_JUMP;
                    case CONDITIONAL_JUMP:
						var condition = Stack.Pop();
						condition.HintType(Stack.DataType.Bool);
						
						var tr = tree;

						if (Instructions[tree.Offset].GetOperandsAsUInt == 0 || CodeOffsetToFunctionOffset(Instructions[tree.Offset].GetJumpOffset) == tree.Offset + 1)
						{
							tree.Statements.Add(new Ast.Drop(this, condition));
							break;
						}

                        while (tr != null)
                        {
                            if (tr is WhileTree)
                            {
                                if (Instructions[tree.Offset].GetJumpOffset == (tr as WhileTree).BreakOffset)
                                {
									var condBreak = new IfTree(this, tree, -1, condition, -1);
									condBreak.Statements.Add(new Ast.Break(this));
									tree.Statements.Add(condBreak);
									goto DONE_COND;
                                }
                            }

                            tr = tr.Parent;
                        }

						var jumpLoc = Instructions[CodeOffsetToFunctionOffset(Instructions[tree.Offset].GetJumpOffset) - 1];
                        if (jumpLoc.IsWhileJump && jumpLoc.GetJumpOffset < Instructions[tree.Offset].Offset)
                        {
							Instructions[CodeOffsetToFunctionOffset(Instructions[tree.Offset].GetJumpOffset) - 1].NopInstruction(); // is this really required?
                            var @while = new WhileTree(this, tree, tree.Offset + 1, condition, Instructions[tree.Offset].GetJumpOffset);
                            treeStack.Push(@while);
                            tree.Statements.Add(@while);
							tree.Offset = CodeOffsetToFunctionOffset(@while.BreakOffset);
                            break;
                        }
						else
						{
							if (Instructions[tree.Offset].GetJumpOffset < Instructions[tree.Offset].Offset)
							{
								//break;
								throw new InvalidOperationException("Do while loops are not supported");
							}

							var @if = new IfTree(this, tree, tree.Offset + 1, condition, Instructions[tree.Offset].GetJumpOffset);
                            treeStack.Push(@if);
                            tree.Statements.Add(@if);
							tree.Offset = CodeOffsetToFunctionOffset(@if.EndOffset);
							break;
						}
DONE_COND:
						break;
					default:
						throw new InvalidOperationException("Opcode not handled");
                }

				if (tree == treeStack.Peek())
					tree.Offset++;
				else
					tree = treeStack.Peek();

				while (tree.IsTreeEnd())
				{
					treeStack.Pop();
					if (treeStack.Count == 0)
						return;
					tree = treeStack.Peek();
					TryOptimizeTree(tree);
				}
            }
		}

		internal static bool IsForLoopPair(Ast.AstToken tk1, Ast.AstToken tk2)
		{
			if (tk1 is Ast.GlobalStore && tk2 is Ast.GlobalStore)
			{
				return (tk1 as Ast.GlobalStore).Index == (tk2 as Ast.GlobalStore).Index;
            }

            if (tk1 is Ast.LocalStore && tk2 is Ast.LocalStore)
            {
                return (tk1 as Ast.LocalStore).Index == (tk2 as Ast.LocalStore).Index;
            }

            if (tk1 is Ast.StaticStore && tk2 is Ast.StaticStore)
            {
                return (tk1 as Ast.StaticStore).Index == (tk2 as Ast.StaticStore).Index;
            }

            return false;
		}

		/// <summary>
		/// 1: Collapse if into else if statements
		/// 2: Try to create for loops
		/// </summary>
		/// <param name="tree"></param>
		internal void TryOptimizeTree(StatementTree tree)
		{
			if (tree.Statements.Count == 0)
				return;

			var lastTree = tree.Statements[^1];
			if (lastTree is IfTree)
			{
				var lastIf = lastTree as IfTree;
				if (lastIf.ElseTree != null)
				{
					var @else = lastIf.ElseTree!;
					if (@else.Statements.Count == 1 && @else.Statements[^1] is IfTree)
					{
						var insideIf = @else.Statements[^1] as IfTree;
						var newElseif = new ElseIfTree(this, lastIf, -1, insideIf.Condition, -1);
                        newElseif.Statements = insideIf.Statements;
                        lastIf.ElseIfTrees.Add(newElseif);
						foreach (var elseIf in insideIf.ElseIfTrees)
						{
							elseIf.Parent = lastIf;
							lastIf.ElseIfTrees.Add(elseIf);
						}
						lastIf.ElseTree = insideIf.ElseTree;
						if (lastIf.ElseTree != null)
							lastIf.ElseTree!.Parent = lastIf;
                    }
                }
			}
			else if (lastTree is WhileTree && tree.Statements.Count > 1)
			{
				var lastWhile = lastTree as WhileTree;
				var lastSet = tree.Statements[^2];
				if (lastWhile.Statements.Count >= 1 && lastWhile.Statements[^1] is Ast.AstToken && lastSet is Ast.AstToken)
				{
					var lastStmtInWhile = lastWhile.Statements[^1] as Ast.AstToken;
                    if (IsForLoopPair(lastSet as Ast.AstToken, lastStmtInWhile))
					{
						var @for = new ForTree(this, tree, -1, lastSet as Ast.AstToken, lastWhile.Condition, lastStmtInWhile);
						lastWhile.Statements.Remove(lastStmtInWhile);
						tree.Statements.Remove(lastWhile);
						tree.Statements.Remove(lastSet);
						@for.Statements = lastWhile.Statements;
						tree.Statements.Add(@for);
						// TODO add support for continue statements (fix jumps)

						if (lastSet is Ast.LocalStore)
						{
							SetFrameVarAutoLoopIdx((lastSet as Ast.LocalStore).Index);
						}
                    }
				}
			}
		}
	}
}
