using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast.StatementTree
{
    internal class Else : Tree
    {
        public readonly int EndOffset;
        public Else(Function function, Tree parent, int offset, int endOffset) : base(function, parent, offset)
        {
            EndOffset = endOffset;
        }

        public override bool IsTreeEnd()
        {
            if (Offset < Function.Instructions.Count && Function.Instructions[Offset].Offset >= EndOffset)
                return true;

            if (Statements.Count > 0)
            {
                if (Statements[^1] is Break /*|| Statements[^1] is Ast.Return*/ || Statements[^1] is Jump)
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            if ((Parent as If).CanSkipBraces())
                return $"else{Environment.NewLine}{ToString(false)}";
            else
                return $"else{Environment.NewLine}{{{Environment.NewLine}{base.ToString()}}}";
        }
    }
}
