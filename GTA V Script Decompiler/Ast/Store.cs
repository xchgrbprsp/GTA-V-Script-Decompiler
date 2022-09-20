using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Store : AstToken
    {
        public readonly AstToken Pointer;
        public readonly AstToken Value;

        public Store(Function func, AstToken pointer, AstToken value) : base(func)
        {
            Pointer = pointer;
            Value = value;

            if (Types.HasPointerVersion(value.GetType()))
                Pointer.HintType(Types.GetPointerVersion(value.GetType()));
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            return "*" + Pointer.ToPointerString() + " = " + Value.ToString() + ";";
        }
    }
}
