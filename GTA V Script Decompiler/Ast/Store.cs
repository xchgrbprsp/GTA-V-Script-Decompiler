using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Store : AstToken
    {
        readonly AstToken Pointer;
        readonly AstToken Value;

        public Store(Function func, AstToken pointer, AstToken value) : base(func)
        {
            Pointer = pointer;
            Value = value;
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            return "*" + Pointer + " = " + Value + ";";
        }
    }
}
