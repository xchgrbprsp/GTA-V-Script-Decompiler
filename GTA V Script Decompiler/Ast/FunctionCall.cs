using System.Collections.Generic;

namespace Decompiler.Ast
{
    internal class FunctionCall : FunctionCallBase
    {
        public readonly Function Callee;
        public FunctionCall(Function func, List<AstToken> arguments, Function callee) : base(func, arguments)
        {
            Callee = callee;

            var i = 0;
            foreach (var arg in arguments)
            {
                if (arg.GetStackCount() == 1)
                {
                    arg.HintType(ref callee.Params.GetVarAtIndex((uint)i).DataType);
                }

                i += arg.GetStackCount();
            }
        }

        public override void HintType(ref TypeContainer container) => Callee.HintReturnType(ref container);

        public override ref TypeContainer GetTypeContainer() => ref Callee.ReturnType;

        public override string GetName() => Callee.Name;

        public override int GetReturnCount() => Callee.NumReturns;
    }
}
