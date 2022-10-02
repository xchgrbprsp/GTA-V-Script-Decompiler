using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Decompiler.IO
{
	public class Reader : BinaryReader
	{
		public Reader(Stream stream)
			: base(stream)
		{
		}

		public void Advance(int size = 4)
		{
			base.BaseStream.Position += size;
		}

		public Int32 ReadPointer()
		{
			return (ReadInt32() & 0xFFFFFF);
		}

		public override string ReadString()
		{
			string temp = "";
			byte next = ReadByte();
			while (next != 0)
			{
				temp += (char)next;
				next = ReadByte();
			}
			return temp;
		}
	}
	public class Writer : BinaryWriter
	{
		public Writer(Stream stream)
			: base(stream)
		{
		}
	}
}
