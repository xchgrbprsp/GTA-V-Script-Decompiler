using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;

namespace Decompiler
{
    class x64NativeFile : Dictionary<ulong, string>
    {
        public ulong TranslateHash(ulong hash)
        {
            if (TranslationTable.ContainsKey(hash))
            {
                hash = TranslationTable[hash];
            }

            return hash;
        }

        public Dictionary<ulong, ulong> TranslationTable = new();
        public x64NativeFile()
            : base()
        {
            StreamReader sr;
            Stream Decompressed = new MemoryStream();
            Stream Compressed = new MemoryStream(Properties.Resources.native_translation);
            DeflateStream deflate = new(Compressed, CompressionMode.Decompress);
            deflate.CopyTo(Decompressed);
            deflate.Dispose();
            Decompressed.Position = 0;
            sr = new StreamReader(Decompressed);
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                if (line.Length > 1)
                {
                    var val = line.Remove(line.IndexOfAny(new char[] { ':', '=' }));
                    var nat = line[(val.Length + 1)..];
                    if (ulong.TryParse(val, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var newer))
                    {
                        if (!ContainsKey(newer))
                        {
                            if (ulong.TryParse(nat, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var older))
                            {
                                if (!TranslationTable.ContainsKey(newer))
                                {
                                    TranslationTable.Add(newer, older);
                                }
                            }
                        }
                    }
                }
            }

            sr.Close();
        }
    }
}
