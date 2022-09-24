using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Attribute : AstToken
    {
        public readonly string Value;
        public readonly bool NeedSemicolon;

        public Attribute(Function func, string value, bool semi = true) : base(func)
        {
            Value = value;
            NeedSemicolon = semi;
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            return $"[[{Value}]]{(NeedSemicolon ? ";" : "")}";
        }
    }
}
