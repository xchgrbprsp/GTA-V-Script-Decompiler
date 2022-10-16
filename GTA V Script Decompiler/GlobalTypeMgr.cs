using System.Collections.Generic;

namespace Decompiler
{
    /// <summary>
    /// Helper class to hold a container in a dictionary
    /// </summary>
    internal class TypeContainerHolder
    {
        private TypeContainer TypeContainer;

        public TypeContainerHolder() => TypeContainer = new();

        public TypeContainerHolder(TypeContainer typeContainer) => TypeContainer = typeContainer;

        public ref TypeContainer GetContainer() => ref TypeContainer;
    }

    internal class GlobalTypeMgr
    {
        private readonly Dictionary<int, TypeContainerHolder> GlobalTypes = new();
        private readonly Dictionary<int, string> GlobalNameOverrides = new();

        public GlobalTypeMgr()
        {
        }

        public ref TypeContainer GetGlobalType(int idx)
        {
            lock (GlobalTypes)
            {
                if (GlobalTypes.TryGetValue(idx, out var type))
                    return ref type.GetContainer();

                GlobalTypes[idx] = new();
                return ref GlobalTypes[idx].GetContainer();
            }
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
