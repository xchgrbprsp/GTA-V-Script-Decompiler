using System;

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
            return (Parent as If).CanSkipBraces()
                ? $"else if ({Condition}){Environment.NewLine}{ToString(false)}"
                : $"else if ({Condition}){Environment.NewLine}{{{Environment.NewLine}{base.ToString()}}}";
        }
    }
}
