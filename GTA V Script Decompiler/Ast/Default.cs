using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Default : AstToken
    {
        public Default(Function func) : base(func)
        {
        }

        public override string ToString()
        {
            return "default";
        }
    }
}
