using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast.StatementTree
{
    internal class ElseIf : Tree
    {
        public readonly AstToken Condition;
        public readonly int EndOffset;
        public ElseIf(Function function, Tree parent, int offset, AstToken condition, int endOffset) : base(function, parent, offset)
        {
            Condition = condition;
            EndOffset = endOffset;
        }

        /// <returns>Isn't going to be called anyway</returns>
        public override bool IsTreeEnd()
        {
            return true;
        }

        public override string ToString()
        {
            if ((Parent as If).CanSkipBraces())
                return $"else if ({Condition.ToString()}){Environment.NewLine}{ToString(false)}";
            else
                return $"else if ({Condition.ToString()}){Environment.NewLine}{{{Environment.NewLine}{base.ToString()}}}";
        }
    }
}
