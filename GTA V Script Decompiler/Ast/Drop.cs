using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Drop : AstToken
    {
        AstToken Dropped;
        public Drop(Function func, AstToken dropped) : base(func)
        {
            Dropped = dropped;
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            return Dropped.ToString() + ";";
        }
    }
}
