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
        }

        public override string GetName()
        {
            return Callee.Name;
        }

        public override int GetReturnCount()
        {
            return Callee.Rcount;
        }
    }
}
