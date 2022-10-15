using System;
using System.Collections.Generic;
using System.Linq;

namespace Decompiler
{
	internal class Instruction
	{
		public Opcode Opcode { get; private set; }
		public Opcode OriginalOpcode { get; private set; }
		public byte[] Operands { get; private set; }

		public Instruction(Opcode Instruction, IEnumerable<byte> Operands, int Offset)
		{
			Opcode = Instruction;
			OriginalOpcode = Opcode;
			this.Operands = Operands.ToArray();
			this.Offset = Offset;
		}

		public Instruction(byte Instruction, IEnumerable<byte> Operands, int Offset)
		{
			Opcode = (Opcode)Instruction;
			OriginalOpcode =  Opcode;
			this.Operands = Operands.ToArray();
			this.Offset = Offset;
		}

		public Instruction(Opcode Instruction, int Offset)
		{
			Opcode = Instruction;
			OriginalOpcode = Opcode;
			Operands = new byte[0];
			this.Offset = Offset;
		}

		public Instruction(byte Instruction, int Offset)
		{
			Opcode = (Opcode)Instruction;
			OriginalOpcode = (Opcode)Instruction;
			Operands = new byte[0];
			this.Offset = Offset;
		}

		public void NopInstruction()
		{
			Opcode = Opcode.NOP;
		}

		public int Offset { get; }

		public int InstructionLength
		{
			get { return 1 + Operands.Length; }
		}

		public int GetOperandsAsInt
		{
			get
			{
				return Operands.Length switch
				{
					1 => Operands[0],
					2 => BitConverter.ToInt16(Operands, 0),
					3 => (Operands[2] << 16) | (Operands[1] << 8) | Operands[0],
					4 => BitConverter.ToInt32(Operands, 0),
					_ => throw new Exception("Invalid amount of operands (" + Operands.Length.ToString() + ")"),
				};
			}
		}

		public float GetFloat
		{
			get
			{
				return Operands.Length != 4 ? throw new Exception("Not a Float") : BitConverter.ToSingle(Operands, 0);
			}
		}

		public byte GetOperand(int index)
		{
			return Operands[index];
		}

		public uint GetOperandsAsUInt
		{
			get
			{
				return Operands.Length switch
				{
					1 => Operands[0],
					2 => BitConverter.ToUInt16(Operands, 0),
					3 => (uint)((Operands[2] << 16) | (Operands[1] << 8) | Operands[0]),
					4 => BitConverter.ToUInt32(Operands, 0),
					_ => throw new Exception("Invalid amount of operands (" + Operands.Length.ToString() + ")"),
				};
			}
		}

		public int GetJumpOffset
		{
			get
			{
				return IsJumpInstruction ? BitConverter.ToInt16(Operands, 0) + Offset + 3 : throw new Exception("Not A jump");
			}
		}

		public byte GetNativeParams
		{
			get
			{
				return Opcode == Opcode.NATIVE ? (byte)(Operands[0] >> 2) : throw new Exception("Not A Native");
			}
		}

		public byte GetNativeReturns
		{
			get
			{
				return Opcode == Opcode.NATIVE ? (byte)(Operands[0] & 0x3) : throw new Exception("Not A Native");
			}
		}

		public ushort GetNativeIndex
		{
			get
			{
				if (Opcode == Opcode.NATIVE)
				{
					// if (_consoleVer)
					return Utils.SwapEndian(BitConverter.ToUInt16(Operands, 1));
					//else
					//	return BitConverter.ToUInt16(operands, 1);
				}

				throw new Exception("Not A Native");
			}
		}

		public int GetSwitchCase(int index)
		{
			if (Opcode == Opcode.SWITCH)
			{
				int cases = GetOperand(0);
				return index >= cases ? throw new Exception("Out of range script case") : BitConverter.ToInt32(Operands, 1 + (index * 6));
			}

			throw new Exception("Not A Switch Statement");
		}

		public string GetSwitchStringCase(int index)
		{
			if (Opcode == Opcode.SWITCH)
			{
				int cases = GetOperand(0);
				return index >= cases
					? throw new Exception("Out of range script case")
					: Program.getIntType == Program.IntType._uint
					? ScriptFile.HashBank.GetHash(BitConverter.ToUInt32(Operands, 1 + (index*6)))
					: ScriptFile.HashBank.GetHash(BitConverter.ToUInt32(Operands, 1 + (index*6)));
			}

			throw new Exception("Not A Switch Statement");
		}

		public int GetSwitchOffset(int index)
		{
			if (Opcode == Opcode.SWITCH)
			{
				int cases = GetOperand(0);
				return index >= cases
					? throw new Exception("Out of range script case")
					: Offset + 8 + (index*6) + BitConverter.ToInt16(Operands, 5 + (index*6));
			}

			throw new Exception("Not A Switch Statement");
		}

		public int GetImmBytePush
		{
			get
			{
				var _instruction = (int)Opcode;
				return _instruction is>=109 and <=117 ? _instruction - 110 : throw new Exception("Not An Immediate Int Push");
			}
		}

		public float GetImmFloatPush
		{
			get
			{
				var _instruction = (int)Opcode;
				return _instruction is>=118 and <=126 ? _instruction - 119 : throw new Exception("Not An Immediate Float Push");
			}
		}

		public bool IsJumpInstruction
		{
			get { return (int)OriginalOpcode is>84 and <93; }
		}

		public bool IsConditionJump
		{
			get { return (int)Opcode is>85 and <93; }
		}

		public bool IsWhileJump
		{
			get
			{
				return Opcode == Opcode.J ? GetJumpOffset <= 0 ? false : GetOperandsAsInt < 0 : false;
			}
		}
	}

	internal enum Opcode //opcodes reversed from gta v eboot.bin
	{
		NOP,
		IADD,
		ISUB,
		IMUL,
		IDIV,
		IMOD,
		INOT,
		INEG,
		IEQ,
		INE,
		IGT,
		IGE,
		ILT,
		ILE,
		FADD,
		FSUB,
		FMUL,
		FDIV,
		FMOD,
		FNEG,
		FEQ,
		FNE,
		FGT,
		FGE,
		FLT,
		FLE,
		VADD,
		VSUB,
		VMUL,
		VDIV,
		VNEG,
		IAND,
		IOR,
		IXOR,
		I2F,
		F2I,
		F2V,
		PUSH_CONST_U8,
		PUSH_CONST_U8_U8,
		PUSH_CONST_U8_U8_U8,
		PUSH_CONST_U32,
		PUSH_CONST_F,
		DUP,
		DROP,
		NATIVE,
		ENTER,
		LEAVE,
		LOAD,
		STORE,
		STORE_REV,
		LOAD_N,
		STORE_N,
		ARRAY_U8,
		ARRAY_U8_LOAD,
		ARRAY_U8_STORE,
		LOCAL_U8,
		LOCAL_U8_LOAD,
		LOCAL_U8_STORE,
		STATIC_U8,
		STATIC_U8_LOAD,
		STATIC_U8_STORE,
		IADD_U8,
		IMUL_U8,
		IOFFSET,
		IOFFSET_U8,
		IOFFSET_U8_LOAD,
		IOFFSET_U8_STORE,
		PUSH_CONST_S16,
		IADD_S16,
		IMUL_S16,
		IOFFSET_S16,
		IOFFSET_S16_LOAD,
		IOFFSET_S16_STORE,
		ARRAY_U16,
		ARRAY_U16_LOAD,
		ARRAY_U16_STORE,
		LOCAL_U16,
		LOCAL_U16_LOAD,
		LOCAL_U16_STORE,
		STATIC_U16,
		STATIC_U16_LOAD,
		STATIC_U16_STORE,
		GLOBAL_U16,
		GLOBAL_U16_LOAD,
		GLOBAL_U16_STORE,
		J,
		JZ,
		IEQ_JZ,
		INE_JZ,
		IGT_JZ,
		IGE_JZ,
		ILT_JZ,
		ILE_JZ,
		CALL,
		GLOBAL_U24,
		GLOBAL_U24_LOAD,
		GLOBAL_U24_STORE,
		PUSH_CONST_U24,
		SWITCH,
		STRING,
		STRINGHASH,
		TEXT_LABEL_ASSIGN_STRING,
		TEXT_LABEL_ASSIGN_INT,
		TEXT_LABEL_APPEND_STRING,
		TEXT_LABEL_APPEND_INT,
		TEXT_LABEL_COPY,
		CATCH,
		THROW,
		CALLINDIRECT,
		PUSH_CONST_M1,
		PUSH_CONST_0,
		PUSH_CONST_1,
		PUSH_CONST_2,
		PUSH_CONST_3,
		PUSH_CONST_4,
		PUSH_CONST_5,
		PUSH_CONST_6,
		PUSH_CONST_7,
		PUSH_CONST_FM1,
		PUSH_CONST_F0,
		PUSH_CONST_F1,
		PUSH_CONST_F2,
		PUSH_CONST_F3,
		PUSH_CONST_F4,
		PUSH_CONST_F5,
		PUSH_CONST_F6,
		PUSH_CONST_F7,
		IS_BIT_SET
	}
}
