using System;

namespace Decompiler.Ast.StatementTree
{
    internal class While : Tree
    {
        public readonly int BreakOffset;
        public readonly AstToken Condition;

        public While(Function function, Tree parent, int offset, AstToken condition, int breakOffset) : base(function, parent, offset)
        {
            BreakOffset = breakOffset;
            Condition = condition;
        }

        public override bool IsTreeEnd()
        {
            if (Offset < Function.Instructions.Count && Function.Instructions[Offset].Offset >= BreakOffset)
                return true;

            if (Statements.Count > 0)
            {
                return Statements[^1] is Break /*|| Statements[^1] is Ast.Return*/ || Statements[^1] is Jump;
            }

            return false;
        }

        public override string ToString()
        {
            return $"while ({Condition}){Environment.NewLine}{{{Environment.NewLine}{base.ToString()}}}";
        }
    }
}
