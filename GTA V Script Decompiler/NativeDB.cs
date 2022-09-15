using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Security.Policy;

namespace Decompiler
{
    internal struct NativeDBParam
    {
        public string type { get; set; }
        public string name { get; set; }
    }
    internal struct NativeDBEntry
    {
        public string name { get; set; }
        public string jhash { get; set; }
        public string comment { get; set; }
        public List<NativeDBParam> @params { get; set; }
        public string return_type { get; set; }
        public string build { get; set; }
        public string[]? old_names { get; set; }
        public bool? unused { get; set; }

        public string @namespace;

        public Stack.DataType GetParamType(int index)
        {
            if (index > @params.Count - 1)
                return Stack.DataType.Unk;
            return Types.GetFromName(@params[index].type);
        }

        public Stack.DataType GetReturnType()
        {
            return Types.GetFromName(return_type);
        }
    }
    internal class NativeDB
    {
        Dictionary<string, Dictionary<string, NativeDBEntry>> data;
        Dictionary<UInt64, NativeDBEntry> entries;

        public void LoadData()
        {
            string file = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "natives.json");

            if (File.Exists(file))
                data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, NativeDBEntry>>>(File.ReadAllText(file));
            else
                data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, NativeDBEntry>>>(Properties.Resources.native_db_json);

            entries = new();

            foreach (var ns in data)
            {
                foreach (var native in ns.Value)
                {
                    NativeDBEntry entry = native.Value;
                    entry.@namespace = ns.Key;
                    entries[Convert.ToUInt64(native.Key, 16)] = entry;
                }
            }
        }

        public NativeDBEntry? GetEntry(UInt64 hash)
        {
            if (entries.ContainsKey(hash))
                return entries[hash];

            return null;
        }
    }
}
