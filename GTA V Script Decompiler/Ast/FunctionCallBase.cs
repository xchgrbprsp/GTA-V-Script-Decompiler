using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal abstract class FunctionCallBase : AstToken
    {
        readonly List<AstToken> Arguments;

        public abstract string GetName();
        public abstract int GetReturnCount();
        protected FunctionCallBase(Function func, List<AstToken> arguments) : base(func)
        {
            Arguments = arguments;
        }

        public override bool IsStatement()
        {
            return GetReturnCount() == 0;
        }

        public override bool HasSideEffects()
        {
            return true;
        }

        public override string ToString()
        {
            bool first = true;
            StringBuilder sb = new();
            sb.Append(GetName());
            sb.Append('(');

            foreach (var arg in Arguments)
            {
                if (!first)
                    sb.Append(", ");
                sb.Append(arg.ToString());
                first = false;
            }
            sb.Append(')');
            if (IsStatement())
                sb.Append(';');
            return sb.ToString();
        }
    }
}
