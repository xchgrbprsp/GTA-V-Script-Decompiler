using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Load : AstToken
    {
        readonly AstToken Pointer;
        public Load(Function func, AstToken pointer) : base(func)
        {
            Pointer = pointer;
        }

        public override string ToString()
        {
            return "*" + Pointer;
        }
    }
}
