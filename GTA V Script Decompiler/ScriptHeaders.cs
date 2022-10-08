using System;
using System.IO;

namespace Decompiler
{
	public class ScriptHeader
	{
		//Header Start
		public int Magic { get; set; }
		public int SubHeader { get; set; } //wtf?
		public int CodeBlocksOffset { get; set; }
		public int GlobalsVersion { get; set; } //Not sure if this is the globals version
		public int CodeLength { get; set; } //Total length of code
		public int ParameterCount { get; set; } //Count of paremeters to the script
		public int StaticsCount { get; set; }
		public int GlobalsCount { get; set; }
		public int NativesCount { get; set; } //Native count * 4 = total block length
		public int StaticsOffset { get; set; }
		public int GlobalsOffset { get; set; }
		public int NativesOffset { get; set; }
		public int Null1 { get; set; } //unknown
		public int Null2 { get; set; } //Unknown
		public int NameHash { get; set; } //Hash of the script name at ScriptNameOffset
		public int Null3 { get; set; }
		public int ScriptNameOffset { get; set; }
		public int StringsOffset { get; set; } //Offset of the string table
		public int StringsSize { get; set; } //Total length of the string block
		public int Null4 { get; set; }
		//Header End

		//Other Vars
		public int RSC7Offset;
		public int[] StringTableOffsets { get; set; }
		public int[] CodeTableOffsets { get; set; }
		public int StringBlocks { get; set; }
		public int CodeBlocks { get; set; }
		public string ScriptName { get; set; }
		public bool isRSC7 { get; private set; }

		public static ScriptHeader Generate(Stream scriptStream)
		{
			ScriptHeader header = new();
			IO.Reader reader = new(scriptStream);
			scriptStream.Seek(0, SeekOrigin.Begin);
			header.RSC7Offset = (reader.ReadUInt32() == 0x37435352) ? 0x10 : 0x0;
			scriptStream.Seek(header.RSC7Offset, SeekOrigin.Begin);
			header.Magic = reader.ReadInt32(); //0x0
			reader.Advance();
			header.SubHeader = reader.ReadPointer(); //0x8
			reader.Advance();
			header.CodeBlocksOffset = reader.ReadPointer(); //0x10
			reader.Advance();
			header.GlobalsVersion = reader.ReadInt32(); //0x18
			header.CodeLength = reader.ReadInt32(); //0x1C
			header.ParameterCount = reader.ReadInt32(); //0x20
			header.StaticsCount = reader.ReadInt32(); //0x24
			header.GlobalsCount = reader.ReadInt32(); //0x28
			header.NativesCount = reader.ReadInt32(); //0x2C
			header.StaticsOffset = reader.ReadPointer(); //0x30
			reader.Advance();
			header.GlobalsOffset = reader.ReadPointer(); //0x38
			reader.Advance();
			header.NativesOffset = reader.ReadPointer(); //0x40
			reader.Advance();
			header.Null1 = reader.ReadInt32(); //0x48
			reader.Advance();
			header.Null2 = reader.ReadInt32(); //0x50
			reader.Advance();
			header.NameHash = reader.ReadInt32(); //0x58
			header.Null3 = reader.ReadInt32(); //0x5C
			header.ScriptNameOffset = reader.ReadPointer(); //0x60
			reader.Advance();
			header.StringsOffset = reader.ReadPointer(); //0x68
			reader.Advance();
			header.StringsSize = reader.ReadInt32(); //0x70
			reader.Advance();
			header.Null4 = reader.ReadInt32(); //0x78
			reader.Advance();

			header.StringBlocks = (header.StringsSize + 0x3FFF) >> 14;
			header.CodeBlocks = (header.CodeLength + 0x3FFF) >> 14;

			header.StringTableOffsets = new int[header.StringBlocks];
			scriptStream.Seek(header.StringsOffset + header.RSC7Offset, SeekOrigin.Begin);
			for (int i = 0; i < header.StringBlocks; i++)
			{
				header.StringTableOffsets[i] = reader.ReadPointer() + header.RSC7Offset;
				reader.Advance();
			}


			header.CodeTableOffsets = new int[header.CodeBlocks];
			scriptStream.Seek(header.CodeBlocksOffset + header.RSC7Offset, SeekOrigin.Begin);
			for (int i = 0; i < header.CodeBlocks; i++)
			{
				header.CodeTableOffsets[i] = reader.ReadPointer() + header.RSC7Offset;
				reader.Advance();
			}
			scriptStream.Position = header.ScriptNameOffset + header.RSC7Offset;
			int data = scriptStream.ReadByte();
			header.ScriptName = "";
			while (data != 0 && data != -1)
			{
				header.ScriptName += (char)data;
				data = scriptStream.ReadByte();
			}
			return header;
		}

		private ScriptHeader()
		{ }
	}

}
