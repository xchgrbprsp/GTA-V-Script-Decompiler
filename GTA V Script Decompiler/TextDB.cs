using System.Collections.Generic;
using System.IO;

namespace Decompiler
{
    /// <summary>
    /// https://github.com/dexyfex/CodeWalker/blob/master/CodeWalker.Core/GameFiles/FileTypes/Gxt2File.cs
    /// </summary>
    internal class TextDB
    {
        public Dictionary<uint, string> Strings = new();

        public TextDB()
        {
            var textsDir = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "texts");

            if (Directory.Exists(textsDir))
            {
                var texts = Directory.GetFiles(textsDir);

                foreach (var text in texts)
                {
                    LoadFromStream(new IO.Reader(File.OpenRead(text)));
                }
            }
            else
            {
                LoadFromStream(new IO.Reader(new MemoryStream(Properties.Resources.globalText)));
            }
        }

        void LoadFromStream(IO.Reader reader)
        {
            List<KeyValuePair<uint, uint>> offsets = new();

            if (reader.ReadUInt32() != 1196971058)
                throw new FileFormatException("Invalid magic");

            var count = reader.ReadUInt32();

            for (var i = 0; i < count; i++)
            {
                offsets.Add(new(reader.ReadUInt32(), reader.ReadUInt32()));
            }

            if (reader.ReadUInt32() != 1196971058)
                throw new FileFormatException("Invalid magic");

            var end = reader.ReadUInt32();

            for (var i = 0; i < count; i++)
            {
                reader.BaseStream.Position = offsets[i].Value;
                var buf = "";

                do
                {
                    var c = reader.ReadChar();

                    if (c == 0)
                        break;

                    buf += c;
                }
                while (reader.BaseStream.Position < end);

                Strings.Add(offsets[i].Key, buf);
            }
        }
    }
}
