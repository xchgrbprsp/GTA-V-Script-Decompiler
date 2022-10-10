using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class FunctionCall : FunctionCallBase
    {
        public readonly Function Callee;
        public FunctionCall(Function func, List<AstToken> arguments, Function callee) : base(func, arguments)
        {
            Callee = callee;

            int i = 0;
            foreach (var arg in arguments)
            {
                if (arg.GetStackCount() == 1)
                {
                    arg.HintType(ref callee.Params.GetVarAtIndex((uint)i).DataType);
                }
                i += arg.GetStackCount();
            }
        }

        public override void HintType(ref TypeContainer container)
        {
            Callee.HintReturnType(ref container);
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Callee.ReturnType;
        }

        public override string GetName()
        {
            return Callee.Name;
        }

        public override int GetReturnCount()
        {
            return Callee.NumReturns;
        }
    }
}
