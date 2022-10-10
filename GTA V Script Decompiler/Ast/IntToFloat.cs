using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class IntToFloat : AstToken
    {
        AstToken Integer;
        public IntToFloat(Function func, AstToken integer) : base(func)
        {
            Integer = integer;
            Integer.HintType(ref Types.INT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.FLOAT.GetContainer();
        }

        public override string ToString()
        {
            return "(float)" + Integer.ToString();
        }
    }
}
