using System.Collections.Generic;
using System.Text;

namespace Decompiler.Ast
{
    internal abstract class FunctionCallBase : AstToken
    {
        public readonly List<AstToken> Arguments;
        public int DroppedReturns { get; private set; } = 0;

        public abstract string GetName();
        public abstract int GetReturnCount();

        protected FunctionCallBase(Function func, List<AstToken> arguments) : base(func) => Arguments = arguments;

        public override bool IsStatement() => GetReturnCount() - DroppedReturns == 0;

        public override int GetStackCount() => GetReturnCount() - DroppedReturns;

        public void DropReturn() => DroppedReturns++;

        public override bool HasSideEffects() => true;

        public override string ToString()
        {
            var first = true;
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
