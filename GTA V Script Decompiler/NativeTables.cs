using System;
using System.Collections.Generic;
using System.IO;

namespace Decompiler
{
    public class NativeTable
    {
        public List<string> NativeNames;
        public List<ulong> NativeHashes;

        public NativeTable(Stream scriptFile, int position, int length, int codeSize)
        {
            IO.Reader reader = new(scriptFile);
            var count = 0;
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

                nat = Program.Crossmap.TranslateHash(Rotl(reader.ReadUInt64(), codeSize + count));
                NativeHashes.Add(nat);
                var entry = Program.NativeDB.GetEntry(nat);

                if (entry != null)
                {
                    var nativeName = "";
                    if (Properties.Settings.Default.ShowNativeNamespace)
                        nativeName += entry?.@namespace + "::";

                    nativeName += entry?.name;

                    if (!Properties.Settings.Default.UppercaseNatives)
                        nativeName = nativeName.ToLower();

                    NativeNames.Add(nativeName);
                }
                else
                {
                    var temps = nat.ToString("X");
                    while (temps.Length < 16)
                        temps = "0" + temps;
                    NativeNames.Add("unk_0x" + temps);
                }

                count++;
            }
        }
        public string[] GetNativeTable()
        {
            List<string> table = new();
            var i = 0;
            foreach (var native in NativeNames)
            {
                table.Add(i++.ToString("X2") + ": " + native);
            }

            return table.ToArray();
        }
        public string[] GetNativeHeader() => NativeNames.ToArray();
        public string GetNativeFromIndex(int index)
        {
            return index < 0
                ? throw new ArgumentOutOfRangeException("Index must be a positive integer")
                : index >= NativeNames.Count
                ? throw new ArgumentOutOfRangeException("Index is greater than native table size")
                : NativeNames[index];
        }

        private static ulong Rotl(ulong hash, int rotate)
        {
            rotate %= 64;
            return (hash << rotate) | (hash >> (64 - rotate));
        }

        public ulong GetNativeHashFromIndex(int index)
        {
            return index < 0
                ? throw new ArgumentOutOfRangeException("Index must be a positive integer")
                : index >= NativeHashes.Count
                ? throw new ArgumentOutOfRangeException("Index is greater than native table size")
                : NativeHashes[index];
        }
        public void Dispose() => NativeNames.Clear();
    }
}
