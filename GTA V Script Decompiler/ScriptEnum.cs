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

        public ScriptEnum(Type type, bool isBitset = false)
        {
            Type = type;
            IsBitset = isBitset;
            Keys = Enum.GetNames(Type).ToArray();
            Values = Enum.GetValues(Type).Cast<int>().ToArray();
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
                for (int i = 0; i < Values.Length; i++)
                {
                    if (Values[i] == idx)
                    {
                        val = Keys[i];
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
