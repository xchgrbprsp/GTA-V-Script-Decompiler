using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class FloatToInt : AstToken
    {
        AstToken Float;
        public FloatToInt(Function func, AstToken @float) : base(func)
        {
            Float = @float;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Int;
        }

        public override string ToString()
        {
            return "(int)" + Float;
        }
    }
}
