using System;
using System.Collections.Generic;
using System.IO;

namespace Decompiler
{
    public class NativeTable
    {
        public List<string> NativeNames;
        public List<ulong> NativeHashes;

        private bool alwaysUseHash = false;

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
                if (alwaysUseHash)
                {
                    nat = Rotl(reader.ReadUInt64(), codeSize + count);
                    NativeHashes.Add(nat);
                    NativeNames.Add(String.Format("unk_0x{0:X16}", nat));
                    count++;
                }
                else
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

                        if (entry.name.Length != 0)
                        {
                            nativeName += entry.name;
                        }
                        else
                        {
                            nativeName += String.Format("_0x{0:X16}", nat);
                        }

                        if (!Properties.Settings.Default.UppercaseNatives)
                            nativeName = nativeName.ToLower();

                        NativeNames.Add(nativeName);
                    }
                    else
                    {
                        NativeNames.Add(String.Format("unk_0x{0:X16}", nat));
                    }

                    count++;
                }
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
