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

            Value.HintType(ref Types.INT.GetContainer());
            Bit.HintType(ref Types.INT.GetContainer());
        }
        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.BOOL.GetContainer();
        }

        public override string ToString()
        {
            return "IS_BIT_SET(" + Value.ToString() + ", " + Bit.ToString() + ")";
        }
    }
}
