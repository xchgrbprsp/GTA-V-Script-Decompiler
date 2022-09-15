using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public Dictionary<ulong, ulong> TranslationTable = new Dictionary<ulong, ulong>();
		public x64NativeFile()
			: base()
		{
			StreamReader sr;
			Stream Decompressed = new MemoryStream();
			Stream Compressed = new MemoryStream(Properties.Resources.native_translation);
			DeflateStream deflate = new DeflateStream(Compressed, CompressionMode.Decompress);
			deflate.CopyTo(Decompressed);
			deflate.Dispose();
			Decompressed.Position = 0;
			sr = new StreamReader(Decompressed);
			while (!sr.EndOfStream)
			{
				string line = sr.ReadLine();
				if (line.Length > 1)
				{
					string val = line.Remove(line.IndexOfAny(new char[] { ':', '=' }));
					string nat = line.Substring(val.Length + 1);
					ulong newer;
					ulong older;
					if (ulong.TryParse(val, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out newer))
					{
						if (!ContainsKey(newer))
						{
							if (ulong.TryParse(nat, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out older))
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
