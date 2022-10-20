using Decompiler.Ast;
using System.Collections.Generic;

namespace Decompiler.Hooks
{
    internal class TernaryInjectorHook : FunctionHook
    {
        public override uint[] Hashes => new uint[] { 0x3EE55A88 };

        public override bool Hook(Function function, List<AstToken> args, Stack stack)
        {
            stack.Push(new TernaryOperator(function, args[0], args[1], args[2]));
            return true;
        }
    }
}
