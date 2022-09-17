using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    /// <summary>
    /// Unused by game scripts
    /// </summary>
    internal class StringHash : AstToken
    {
        public readonly String @string;

        public StringHash(Function func, String @string) : base(func)
        {
            this.@string = @string;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Int;
        }

        public override string ToString()
        {
            return "joaat(" + @string.ToString() + ")";
        }
    }
}
