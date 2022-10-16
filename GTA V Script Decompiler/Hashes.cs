using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Decompiler
{
	public class Hashes
	{
		private readonly Dictionary<int, string> hashes;

		public Hashes()
		{
			hashes = new Dictionary<int, string>();
			var reader = File.Exists(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
					"entities.dat"))
				? new StreamReader(
						File.OpenRead(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
							"entities.dat")))
				: (new(new MemoryStream(Properties.Resources.Entities)));

			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();
				hashes[(int)Utils.Joaat(line)] = line;
			}
		}

		public void Export_Entities()
		{
			Stream Decompressed =
				File.Create(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
					"entities_exp.dat"));
			Stream Compressed = new MemoryStream(Properties.Resources.Entities);
			DeflateStream deflate = new(Compressed, CompressionMode.Decompress);
			deflate.CopyTo(Decompressed);
			deflate.Dispose();
			Decompressed.Close();
			System.Diagnostics.Process.Start(
				Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)));

		}

		public string GetHash(uint value)
		{
			if (Program.ShouldReverseHashes && value > 200)
			{
				var intvalue = (int)value;
				if (hashes.TryGetValue(intvalue, out var val))
					return "joaat(\"" + val + "\")";
			}

			return Program.getIntType == Program.IntType._int
				? ((int)value).ToString()
				: Program.getIntType == Program.IntType._uint
				? value.ToString()
				: Program.getIntType == Program.IntType._hex ? "0x" + value.ToString("X") : throw new IndexOutOfRangeException();
		}

		public bool IsKnownHash(int value) => hashes.ContainsKey(value);
	}
}
