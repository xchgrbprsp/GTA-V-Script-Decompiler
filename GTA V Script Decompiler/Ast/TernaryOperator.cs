using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Ternary : AstToken
    {
        public readonly AstToken Condition;
        public readonly AstToken ValueIfTrue;
        public readonly AstToken ValueIfFalse;

        public Ternary(Function func, AstToken condition, AstToken valueIfTrue, AstToken valueIfFalse) : base(func)
        {
            Condition = condition;
            ValueIfTrue = valueIfTrue;
            ValueIfFalse = valueIfFalse;

            Condition.HintType(ref Types.BOOL.GetContainer());
            ValueIfTrue.HintType(ref ValueIfFalse.GetTypeContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref ValueIfTrue.GetTypeContainer();
        }

        public override string ToString()
        {
            return $"{Condition} ? {ValueIfTrue} : {ValueIfFalse}";
        }
    }
}
