using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Decompiler
{
	public class Hashes
	{
		Dictionary<int, string> hashes;

		public Hashes()
		{
			hashes = new Dictionary<int, string>();
			StreamReader reader;
			if (
				File.Exists(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
					"entities.dat")))
			{
				reader =
					new StreamReader(
						File.OpenRead(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
							"entities.dat")));
			}
			else
			{
				Stream Decompressed = new MemoryStream();
				Stream Compressed = new MemoryStream(Properties.Resources.Entities);
				DeflateStream deflate = new(Compressed, CompressionMode.Decompress);
				deflate.CopyTo(Decompressed);
				deflate.Dispose();
				Decompressed.Position = 0;
				reader = new StreamReader(Decompressed);
			}
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				string[] split = line.Split(new char[] {':'}, StringSplitOptions.RemoveEmptyEntries);
				if (split.Length != 2)
					continue;
				int hash = 0;
				try
				{
					hash = Convert.ToInt32(split[0]);
				}
				catch
				{
					hash = (int) Convert.ToUInt32(split[0]);
				}
				if (!hashes.ContainsKey(hash) && hash != 0)
					hashes.Add(hash, split[1]);
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
			if (Program.shouldReverseHashes && value > 200)
			{
				int intvalue = (int)value;
				if (hashes.TryGetValue(intvalue, out var val))
					return "joaat(\"" + val + "\")";
			}

			if (Program.getIntType == Program.IntType._int)
				return ((int)value).ToString();
			else if (Program.getIntType == Program.IntType._uint)
				return (value).ToString();
			else if (Program.getIntType == Program.IntType._hex)
				return "0x" + value.ToString("X");
			else
				throw new IndexOutOfRangeException();
		}

		public bool IsKnownHash(int value)
		{
			return hashes.ContainsKey(value);
		}
	}
}
