using System;

namespace Decompiler.Ast.StatementTree
{
    internal class Else : Tree
    {
        public readonly int EndOffset;
        public Else(Function function, Tree parent, int offset, int endOffset) : base(function, parent, offset) => EndOffset = endOffset;

        public override bool IsTreeEnd()
        {
            if (Offset < Function.Instructions.Count && Function.Instructions[Offset].Offset >= EndOffset)
                return true;

            if (Statements.Count > 0)
            {
                if (Statements[^1] is Break /*|| Statements[^1] is Ast.Return*/ or Jump)
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return (Parent as If).CanSkipBraces()
                ? $"else{Environment.NewLine}{ToString(false)}"
                : $"else{Environment.NewLine}{{{Environment.NewLine}{base.ToString()}}}";
        }
    }
}
