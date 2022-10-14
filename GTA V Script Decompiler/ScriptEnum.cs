using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler
{
    public class ScriptEnum
    {
        public Type Type;
        public bool IsBitset;
        string[] Keys;
        int[] Values;
        Dictionary<int, string> CachedValues = new();

        public ScriptEnum(Type type, bool isBitset = false)
        {
            Type = type;
            IsBitset = isBitset;
            Keys = Enum.GetNames(Type).ToArray();
            Values = Enum.GetValues(Type).Cast<int>().ToArray();

            if (!isBitset)
                for (int i = 0; i < Keys.Length; i++)
                    CachedValues[Values[i]] = Keys[i];
        }

        public bool TryGetValue(int idx, out string val)
        {
            val = "";

            if (IsBitset)
            {
                string buf = "";
                bool first = true;

                for (int i = 0; i < Values.Length; i++)
                {
                    if ((Values[i] & idx) != 0)
                    {
                        if (first)
                            buf = Keys[i];
                        else
                            buf += " | " + Values[i];

                        idx &= ~(Values[i]);

                        first = false;
                    }
                }

                if (first)
                    return false;

                if (idx != 0)
                    buf += " | " + val;

                val = buf;
                return true;
            }
            else
            {
                return CachedValues.TryGetValue(idx, out val);
            }
        }
    }
}
