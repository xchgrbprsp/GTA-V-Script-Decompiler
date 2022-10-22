using Decompiler.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Decompiler.Hooks
{
    internal abstract class NativeHook
    {
        public NativeHook()
        {
        }

        public abstract string Native { get; }

        public abstract bool Hook(Function func, List<AstToken> args, Stack stack);

        public static NativeHook[] GetHooks()
        {
            return Assembly
                .GetAssembly(typeof(NativeHook))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(NativeHook))).Select(t => Activator.CreateInstance(t)).Cast<NativeHook>().ToArray();
        }
    }
}
