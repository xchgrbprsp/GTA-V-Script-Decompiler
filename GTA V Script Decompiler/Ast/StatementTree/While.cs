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
            return Offset < Function.Instructions.Count && Function.Instructions[Offset].Offset >= BreakOffset
                ? true
                : Statements.Count > 0 ? Statements[^1] is Break /*|| Statements[^1] is Ast.Return*/ or Jump : false;
        }

        public override string ToString()
        {
            return $"while ({Condition}){Environment.NewLine}{{{Environment.NewLine}{base.ToString()}}}";
        }
    }
}
