using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Decompiler
{
	public class X64NativeTable
	{
		public List<string> NativeNames;
		public List<ulong> NativeHashes;

		public X64NativeTable(Stream scriptFile, int position, int length, int codeSize)
		{
			IO.Reader reader = new IO.Reader(scriptFile);
			int count = 0;
			ulong nat;
			reader.BaseStream.Position = position;
			NativeNames = new List<string>();
			NativeHashes = new List<ulong>();
			while (count < length)
			{
				// GTA V PC natives arent stored sequentially in the table. Each native needs a bitwise rotate depending on its position and codetable size
				// Then the natives needs to go back through translation tables to get to their hash as defined in the vanilla game version
				// or the earliest game version that native was introduced in.
				// Just some of the steps Rockstar take to make reverse engineering harder

				nat = Program.x64nativefile.TranslateHash(Rotl(reader.ReadUInt64(), codeSize + count));
				NativeHashes.Add(nat);
				NativeDBEntry? entry = Program.nativeDB.GetEntry(nat);

				if (entry != null)
				{
					string nativeName = "";
					if (Properties.Settings.Default.ShowNativeNamespace)
						nativeName += entry?.@namespace + "::";

					nativeName += entry?.name;

					if (!Properties.Settings.Default.UppercaseNatives)
						nativeName = nativeName.ToLower();

                    NativeNames.Add(nativeName);
				}
				else
				{
					string temps = nat.ToString("X");
					while (temps.Length < 16)
						temps = "0" + temps;
					NativeNames.Add("unk_0x" + temps);
				}

				count++;
			}

		}
		public string[] GetNativeTable()
		{
			List<string> table = new List<string>();
			int i = 0;
			foreach (string native in NativeNames)
			{
				table.Add(i++.ToString("X2") + ": " + native);
			}
			return table.ToArray();
		}
		public string[] GetNativeHeader()
		{
			return NativeNames.ToArray();
		}
		public string GetNativeFromIndex(int index)
		{
			if (index < 0)
				throw new ArgumentOutOfRangeException("Index must be a positive integer");
			if (index >= NativeNames.Count)
				throw new ArgumentOutOfRangeException("Index is greater than native table size");
			return NativeNames[index];
		}

		private static ulong Rotl(ulong hash, int rotate)
		{
			rotate %= 64;
			return hash << rotate | hash >> (64 - rotate);
		}
		public ulong GetNativeHashFromIndex(int index)
		{
			if (index < 0)
				throw new ArgumentOutOfRangeException("Index must be a positive integer");
			if (index >= NativeHashes.Count)
				throw new ArgumentOutOfRangeException("Index is greater than native table size");
			return NativeHashes[index];
		}
		public void Dispose()
		{
			NativeNames.Clear();
		}
	}
}
