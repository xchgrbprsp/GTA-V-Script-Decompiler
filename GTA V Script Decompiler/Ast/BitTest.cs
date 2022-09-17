using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class BitTest : AstToken
    {
        public readonly AstToken Value;
        public readonly AstToken Bit;
        public BitTest(Function func, AstToken value, AstToken bit) : base(func)
        {
            Value = value;
            Bit = bit;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Bool;
        }

        public override string ToString()
        {
            return "BitTest(" + Value.ToString() + ", " + Bit.ToString() + ");";
        }
    }
}
