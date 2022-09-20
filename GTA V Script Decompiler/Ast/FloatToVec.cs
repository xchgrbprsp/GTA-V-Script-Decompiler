using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    /// <summary>
    /// Required because it is incorrect to dup values with side effects
    /// </summary>
    internal class FloatToVec : AstToken
    {
        public readonly AstToken Float;
        public FloatToVec(Function func, AstToken @float) : base(func)
        {
            Float = @float;

            Float.HintType(Stack.DataType.Float);
        }

        public override int GetStackCount()
        {
            return 3;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Vector3;
        }

        public override string ToString()
        {
            return "FtoV(" + Float.ToString() + ")";
        }
    }
}
