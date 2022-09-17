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
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Float;
        }

        public override string ToString()
        {
            return "(float)" + Integer;
        }
    }
}
