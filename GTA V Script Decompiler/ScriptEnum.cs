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

        public ScriptEnum(Type type, bool isBitset = false)
        {
            Type = type;
            IsBitset = isBitset;
        }

        public bool HasValue(int val)
        {
            var values = Enum.GetValues(Type).Cast<int>();

            if (IsBitset)
            {
                foreach (var value in values)
                    if ((val & value) != 0)
                        return true;

                return false;
            }
            else
            {
                return values.Contains(val);
            }
        }

        public string GetValue(int val)
        {
            var keys = Enum.GetNames(Type).ToArray();
            var values = Enum.GetValues(Type).Cast<int>().ToArray();

            if (IsBitset)
            {
                string buf = "";
                bool first = true;

                for (int i = 0; i < values.Length; i++)
                {
                    if ((values[i] & val) != 0)
                    {
                        if (first)
                            buf = keys[i];
                        else
                            buf += " | " + keys[i];

                        val &= ~(values[i]);

                        first = false;
                    }
                }

                if (val != 0)
                    buf += " | " + val;

                return buf;
            }
            else
            {
                for (int i = 0; i < values.Length; i++)
                    if (values[i] == val)
                        return keys[i];
            }

            return null;
        }
    }
}
