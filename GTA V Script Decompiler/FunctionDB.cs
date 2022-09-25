using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler
{
    internal class FunctionModEntry
    {
        public string? Name = null;
        public Stack.DataType? ReturnType = null;
        public Dictionary<int, string> ParamNames = new();
        public Dictionary<int, Stack.DataType> ParamTypes = new();

        public FunctionModEntry()
        {
        }
    }

    internal class FunctionDB
    {
        Dictionary<uint, FunctionModEntry> Entries = new();

        private void EnsureEntryCreated(uint hash)
        {
            if (!Entries.ContainsKey(hash))
                Entries[hash] = new();
        }

        public FunctionDB()
        {
            string file = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "functions.db");

            string[] lines;

            if (File.Exists(file))
                lines = File.ReadAllLines(file);
            else
                lines = Encoding.Default.GetString(Properties.Resources.Functions).Trim().Split("\r\n");

            lines = lines.Select(x => x.Trim()).ToArray();

            foreach (var line in lines)
            {
                if (line.StartsWith("#") || line.Length == 0)
                    continue;

                var tokens = line.Split(" ");
                tokens = tokens.Where(x => x.Length != 0).ToArray();

                uint hash;
                if (!uint.TryParse(tokens[0].Replace("0x", ""), System.Globalization.NumberStyles.HexNumber, null, out hash))
                    throw new FileFormatException("Cannot parse function hash");

                EnsureEntryCreated(hash);

                tokens[1] = tokens[1].ToUpper();

                switch (tokens[1])
                {
                    case "NAME":
                        Entries[hash].Name = tokens[2];
                        break;
                    case "RETURN":
                        Entries[hash].ReturnType = Types.GetFromName(tokens[2]);
                        break;
                    case "ARG_NAME":
                        Entries[hash].ParamNames.Add(int.Parse(tokens[2]), tokens[3]);
                        break;
                    case "ARG_TYPE":
                        Entries[hash].ParamTypes.Add(int.Parse(tokens[2]), Types.GetFromName(tokens[3]));
                        break;
                    default:
                        throw new FileFormatException("Unknown field");
                }
            }
        }

        public void Visit(Function func)
        {
            uint hash = func.Hash;

            if (Entries.ContainsKey(hash))
            {
                var entry = Entries[hash];

                if (entry.Name != null)
                {
                    func.Name = entry.Name!;
                }

                if (entry.ReturnType != null)
                {
                    func.HintReturnType(entry.ReturnType.Value);
                }

                foreach (var p in entry.ParamNames)
                {
                    func.GetFrameVar((uint)p.Key).Name = p.Value;
                }

                foreach (var p in entry.ParamTypes)
                {
                    func.GetFrameVar((uint)p.Key).HintType(p.Value);
                }
            }
        }
    }
}
