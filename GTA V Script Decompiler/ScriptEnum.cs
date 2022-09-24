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
        public ScriptEnum(Type type)
        {
            Type = type;
        }

        public bool HasValue(int val)
        {
            return Enum.GetValues(Type).Cast<int>().Contains(val);
        }

        public string GetValue(int val)
        {
            var keys = Enum.GetNames(Type).ToArray();
            var values = Enum.GetValues(Type).Cast<int>().ToArray();

            for (int i = 0; i < values.Count(); i++)
                if (values[i] == val)
                    return keys[i];

            return null;
        }
    }
}
