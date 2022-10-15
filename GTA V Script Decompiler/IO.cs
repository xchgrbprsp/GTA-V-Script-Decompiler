using System.IO;

namespace Decompiler.IO
{
    public class Reader : BinaryReader
    {
        public Reader(Stream stream)
            : base(stream)
        {
        }

        public void Advance(int size = 4) => base.BaseStream.Position += size;

        public int ReadPointer() => ReadInt32() & 0xFFFFFF;

        public override string ReadString()
        {
            var temp = "";
            var next = ReadByte();
            while (next != 0)
            {
                temp += (char)next;
                next = ReadByte();
            }

            return temp;
        }
    }
}
