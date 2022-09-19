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
        public readonly AstToken @string;

        public StringHash(Function func, AstToken @string) : base(func)
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
