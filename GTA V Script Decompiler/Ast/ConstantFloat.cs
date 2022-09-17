using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class ConstantFloat : AstToken
    {
        readonly float Value;
        public ConstantFloat(Function func, float value) : base(func)
        {
            Value = value;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Float;
        }

        public override string ToString()
        {
            return Value.ToString() + "f";
        }
    }
}
