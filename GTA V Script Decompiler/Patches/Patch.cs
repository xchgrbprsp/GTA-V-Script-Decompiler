using System;
using System.Linq;
using System.Reflection;

namespace Decompiler.Patches
{
    internal abstract class Patch
    {
        protected Function Function;

        public Patch(Function function)
        {
            Function = function;
        }

        public static Patch[] GetPatches(Function func)
        {
            return Assembly
                .GetAssembly(typeof(Patch))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Patch))).Select(t => Activator.CreateInstance(t, func)).Cast<Patch>().ToArray();
        }

        public abstract string GetName(int start, int end);
        public abstract bool ShouldShowPatch(int start, int end);
        public abstract bool ShouldEnablePatch(int start, int end);
        public abstract byte[] GetPatch(int start, int end);

        public virtual bool GetData(int start, int end)
        {
            return true;
        }

        public virtual void Reset() { }
    }
}
