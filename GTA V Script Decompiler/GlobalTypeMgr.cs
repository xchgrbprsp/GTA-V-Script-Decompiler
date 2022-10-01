using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler
{
    internal class GlobalTypeMgr
    {
        Dictionary<int, Stack.DataType> GlobalTypes = new();
        Dictionary<int, string> GlobalNameOverrides = new();

        public GlobalTypeMgr()
        {
        }

        public Stack.DataType GetGlobalType(int idx)
        {
            if (GlobalTypes.TryGetValue(idx, out var type))
                return type;

            return Stack.DataType.Unk;
        }

        public void HintGlobalType(int idx, Stack.DataType type)
        {
            var curType = GetGlobalType(idx);

            if (Types.GetTypeInfo(type) > Types.GetTypeInfo(curType))
            {
                GlobalTypes[idx] = type;
            }
        }

        public void Reset()
        {
            GlobalTypes.Clear();
            GlobalNameOverrides.Clear();
        }
    }
}
