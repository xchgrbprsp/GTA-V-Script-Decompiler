using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler
{
    /// <summary>
    /// Helper class to hold a container in a dictionary
    /// </summary>
    internal class TypeContainerHolder
    {
        TypeContainer TypeContainer = new();

        public ref TypeContainer GetContainer()
        {
            return ref TypeContainer;
        }
    }

    internal class GlobalTypeMgr
    {
        Dictionary<int, TypeContainerHolder> GlobalTypes = new();
        Dictionary<int, string> GlobalNameOverrides = new();

        public GlobalTypeMgr()
        {
        }

        public ref TypeContainer GetGlobalType(int idx)
        {
            if (GlobalTypes.TryGetValue(idx, out var type))
                return ref type.GetContainer();

            GlobalTypes[idx] = new();
            return ref GlobalTypes[idx].GetContainer();
        }

        public void HintGlobalType(int idx, ref TypeContainer container)
        {
            var curType = GetGlobalType(idx);

            curType.HintType(ref container);
        }

        public void Reset()
        {
            GlobalTypes.Clear();
            GlobalNameOverrides.Clear();
        }
    }
}
