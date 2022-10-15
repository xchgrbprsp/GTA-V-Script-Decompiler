using System;
using System.Collections.Generic;
using System.Text;

namespace Decompiler
{
	/// <summary>
	/// This is what i use for detecting if a variable is a int/float/bool/struct/array etc
	/// </summary>
	public class VariableStorage
	{
		readonly ListType listType;//static/function_var/parameter
		public List<Variable> Vars;
		Dictionary<int, int> VarRemapper; //not necessary, just shifts variables up if variables before are bigger than 1 DWORD
		private readonly int count;
		private int scriptParamCount = 0;
		private int scriptParamStart { get { return Vars.Count - scriptParamCount; } }
		public VariableStorage(ListType type, int varcount)
		{
			listType = type;
			Vars = new List<Variable>();
			for (var i = 0; i < varcount; i++)
			{
				Vars.Add(new Variable(i));
			}

			count = varcount;
		}
		public VariableStorage(ListType type)
		{
			listType = type;
			Vars = new List<Variable>();
		}
		public void AddVar(int value)
		{
			Vars.Add(new Variable(Vars.Count, value));//only used for static variables that are pre assigned
		}

		public void AddVar(long value)
		{
			Vars.Add(new Variable(Vars.Count, value));
		}
		public void checkvars()
		{
			unusedcheck();
		}
		//This shouldnt be needed but in gamever 1.0.757.2
		//It seems a few of the scripts are accessing items from the
		//Stack frame that they havent reserver
		void BrokenCheck(uint index)
		{
			if (index >= Vars.Count)
			{
				for (var i = Vars.Count; i <= index; i++)
				{
					Vars.Add(new Variable(i));
				}
			}
		}
		public string GetVarName(uint index)
		{
			var var = Vars[(int)index];

			if (var.Name != "")
				return var.Name;

			var name = "";

			if (var.DataType.Type == Types.STRING)
			{
				name = "c";
			}
			else if (var.ImmediateSize == 1)
			{
				name = Vars[(int)index].DataType.Type.Prefix;
			}

			switch (listType)
			{
				case ListType.Statics: name += index >= scriptParamStart ? "ScriptParam_" : "Local_"; break;
				case ListType.Vars: name += "Var"; break;
				case ListType.Params: name += "Param"; break;
			}

			try
			{
				return Program.shouldShiftVariables
					? name + VarRemapper[(int)index].ToString()
					: name + (listType == ListType.Statics && index >= scriptParamStart ? index - scriptParamStart : index).ToString();
			}
			catch (KeyNotFoundException)
			{
				return name + (listType == ListType.Statics && index >= scriptParamStart ? index - scriptParamStart : index).ToString();
			}
		}

		public void SetScriptParamCount(int count)
		{
			if (listType == ListType.Statics)
			{
				scriptParamCount = count;
			}
		}

		public string[] GetDeclaration()
		{
			List<string> Working = new();
			var varName = "";
			var dataType = "";

			var i = 0;
			var j = -1;
			foreach (var var in Vars)
			{
				varName = GetVarName((uint)i);
				j++;
				if (!var.Is_Used)
				{
					if (!Program.shouldShiftVariables)
						i++;
					continue;
				}

				if (listType == ListType.Vars && !var.Is_Called)
				{
					if (!Program.shouldShiftVariables)
						i++;
					continue;
				}

				dataType =var.ImmediateSize == 1
					? var.DataType.Type.VarDec
					: var.DataType.Type == Types.STRING
						? "char "
						: var.DataType.Type == Types.VEC3 ? "Vector3 " : "struct<" + var.ImmediateSize.ToString() + "> ";

				var value = "";

				if (!var.Is_Array)
				{
					if (listType == ListType.Statics)
					{
						if (var.ImmediateSize == 1)
						{
							value = " = " + Utils.Represent(Vars[j].Value, var.DataType.Type);
						}
						else if (var.DataType.Type == Types.STRING)
						{

							List<byte> data = new();

							for (var l = 0; l < var.ImmediateSize; l++)
							{
								data.AddRange(BitConverter.GetBytes(Vars[j + l].Value));
							}

							var len = data.IndexOf(0);
							data.RemoveRange(len, data.Count - len);
							value = " = \"" + Encoding.ASCII.GetString(data.ToArray()) + "\"";

						}
						else if (var.ImmediateSize > 1)
						{
							value += " = { " + Utils.Represent(Vars[j].Value, Types.INT);

							for (var l = 1; l < var.ImmediateSize; l++)
							{
								value += ", " + Utils.Represent(Vars[j + l].Value, Types.INT);
							}

							value += " } ";
						}
					}
				}
				else
				{
					if (listType == ListType.Statics)
					{
						if (var.ImmediateSize == 1)
						{
							value = " = { ";

							for (var k = 0; k < var.Value; k++)
							{
								value += Utils.Represent(Vars[j + 1 + k].Value, var.DataType.Type) + ", ";
							}

							if (value.Length > 2)
							{
								value = value.Remove(value.Length - 2);
							}

							value += " }";
						}
						else if (var.DataType.Type == Types.STRING)
						{
							value = " = { ";

							for (var k = 0; k < var.Value; k++)
							{
								List<byte> data = new();
								for (var l = 0; l < var.ImmediateSize; l++)
								{
									data.AddRange(BitConverter.GetBytes(Vars[j + 1 + (var.ImmediateSize * k) + l].Value));
								}

								value += "\"" + Encoding.ASCII.GetString(data.ToArray()) + "\", ";
							}

							if (value.Length > 2)
							{
								value = value.Remove(value.Length - 2);
							}

							value += " }";
						}
					}
				}

				var decl = dataType + varName;

				if (var.Is_Array)
				{
					decl += "[" + var.Value.ToString() + "]";
				}

				if (var.DataType.Type == Types.STRING)
				{
					decl += "[" + (var.ImmediateSize*8).ToString() + "]";
				}

				Working.Add(decl + value + ";");
				i++;
			}

			return Working.ToArray();
		}

		public string GetPDec()
		{
			if (listType != ListType.Params)
				throw new DecompilingException("Only params use this declaration");
			var decl = "";
			var i = 0;
			foreach (var var in Vars)
			{
				if (!var.Is_Used)
				{
					if (!Program.shouldShiftVariables)
					{
						i++;
					}

					continue;
				}

				var datatype = !var.Is_Array
					? var.DataType.Type == Types.STRING
						? "char[" + (var.ImmediateSize * 4).ToString() + "] c"
						: var.ImmediateSize == 1
						? var.DataType.Type.VarDec + (var.Name.Length == 0 ? var.DataType.Type.Prefix : "")
						: var.DataType.Type == Types.VEC3
							? "Vector3 " + (var.Name.Length == 0 ? var.DataType.Type.Prefix : "")
							: "struct<" + var.ImmediateSize.ToString() + "> "
					: var.DataType.Type == Types.STRING
						? "char[" + (var.ImmediateSize * 4).ToString() + "][] c"
						: var.ImmediateSize == 1 ? var.DataType.Type.ArrayDec : "struct<" + var.ImmediateSize.ToString() + ">[] ";
				decl += datatype + (var.Name == "" ? ("Param" + i.ToString()) : var.Name) + ", ";
				i++;
			}

			if (decl.Length > 2)
				decl = decl.Remove(decl.Length - 2);
			return decl;
		}

		/// <summary>
		/// Remove unused vars from declaration, and shift var indexes down
		/// </summary>
		private void unusedcheck()
		{
			VarRemapper = new Dictionary<int, int>();
			for (int i = 0, k = 0; i < Vars.Count; i++)
			{
				if (!Vars[i].Is_Used)
					continue;
				if (listType == ListType.Vars && !Vars[i].Is_Called)
					continue;
				if (Vars[i].Is_Array)
				{
					for (var j = i + 1; j < i + 1 + (Vars[i].Value * Vars[i].ImmediateSize); j++)
					{
						Vars[j].SetNotUsed();
					}
				}
				else if (Vars[i].ImmediateSize > 1)
				{
					for (var j = i + 1; j < i + Vars[i].ImmediateSize; j++)
					{
						BrokenCheck((uint)j);
						Vars[j].SetNotUsed();
					}
				}

				VarRemapper.Add(i, k);
				k++;
			}
		}

		public Variable GetVarAtIndex(uint index)
		{
			BrokenCheck(index);
			return Vars[(int)index];
		}

		public enum ListType
		{
			Statics,
			Params,
			Vars
		}
	}
}
