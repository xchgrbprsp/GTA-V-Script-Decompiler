using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Vector : AstToken
    {
        public readonly AstToken x;
        public readonly AstToken y;
        public readonly AstToken z;
        public Vector(Function func, AstToken x, AstToken y, AstToken z) : base(func)
        {
            this.x = x;
            this.y = y;
            this.z = z;
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
            return "{ " + x + ", " + y + ", " + z + " }";
        }
    }
}
