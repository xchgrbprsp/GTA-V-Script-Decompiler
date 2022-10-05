using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        abstract public string GetName(int start, int end);
        abstract public bool ShouldShowPatch(int start, int end);
        abstract public bool ShouldEnablePatch(int start, int end);
        abstract public byte[] GetPatch(int start, int end);

        virtual public bool GetData(int start, int end)
        {
            return true;
        }

        virtual public void Reset() { }
    }
}
