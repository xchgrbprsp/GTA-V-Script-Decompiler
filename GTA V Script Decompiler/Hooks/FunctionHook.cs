using Decompiler.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Decompiler.Hooks
{
    internal abstract class FunctionHook
    {
        public FunctionHook()
        {
        }

        public abstract uint[] Hashes { get; }
        public abstract bool Hook(Function function, List<AstToken> args, Stack stack);

        public static FunctionHook[] GetHooks()
        {
            return Assembly
                .GetAssembly(typeof(FunctionHook))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(FunctionHook))).Select(t => Activator.CreateInstance(t)).Cast<FunctionHook>().ToArray();
        }
    }
}
