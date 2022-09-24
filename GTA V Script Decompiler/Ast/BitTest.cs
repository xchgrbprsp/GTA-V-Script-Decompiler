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
        public BitTest(Function func, AstToken bit, AstToken value) : base(func)
        {
            Value = value;
            Bit = bit;

            Value.HintType(Stack.DataType.Int);
            Bit.HintType(Stack.DataType.Int);
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Bool;
        }

        public override string ToString()
        {
            return "IS_BIT_SET(" + Value.ToString() + ", " + Bit.ToString() + ")";
        }
    }
}
