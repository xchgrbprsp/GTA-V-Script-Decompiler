using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Decompiler
{
    internal class NativeDBParam
    {
        public string type { get; set; }
        public string name { get; set; }

        public bool AutoName;
        public Types.TypeInfo TypeInfo;
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

        public Types.TypeInfo ReturnTypeInfo;

        public NativeDBParam? GetParam(int index) => index >= @params.Count ? null : @params[index];

        public Types.TypeInfo GetParamType(int index) => index > @params.Count - 1 ? Types.UNKNOWN : @params[index].TypeInfo;

        public void SetParamType(int index, Types.TypeInfo type)
        {
            var param = @params[index];
            param.type = type.SingleName;
            param.TypeInfo = type;
            @params[index] = param;
        }

        public Types.TypeInfo GetReturnType() => ReturnTypeInfo;

        public void SetReturnType(Types.TypeInfo type)
        {
            return_type = type.SingleName;
            ReturnTypeInfo = type;
        }
    }

    internal class NativeDB
    {
        private Dictionary<string, Dictionary<string, NativeDBEntry>> data;
        private Dictionary<ulong, NativeDBEntry> entries;

        public static bool CanBeUsedAsAutoName(string param)
        {
            if (param.StartsWith("p") && param.Length < 3)
                return false;

            if (param.Contains("unk"))
                return false;

            //if (param == "toggle" || param == "enable")
            //    return false; // TODO

            if (param == "string")
                return false;

            foreach (var type in Types.typeInfos)
                if (type.AutoName == param)
                    return false;

            return true;
        }

        public void LoadData()
        {
            var file = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "natives.json");

            data =File.Exists(file)
                ? JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, NativeDBEntry>>>(File.ReadAllText(file))
                : JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, NativeDBEntry>>>(Properties.Resources.native_db_json);

            entries = new();

            NativeTypeOverride.Initialize();

            foreach (var ns in data)
            {
                foreach (var native in ns.Value)
                {
                    var entry = native.Value;
                    NativeTypeOverride.Visit(ref entry);
                    entry.@namespace = ns.Key;

                    foreach (var param in entry.@params)
                    {
                        param.TypeInfo = Types.GetFromName(param.type);
                        param.AutoName = CanBeUsedAsAutoName(param.name);
                    }

                    entry.ReturnTypeInfo = Types.GetFromName(entry.return_type);

                    entries[Convert.ToUInt64(native.Key, 16)] = entry;
                }
            }
        }

        public NativeDBEntry? GetEntry(ulong hash) => entries.ContainsKey(hash) ? entries[hash] : null;
    }
}
