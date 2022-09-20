using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Break : AstToken
    {
        public Break(Function func) : base(func)
        {
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            return "break;";
        }
    }
}
