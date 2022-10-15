using System;
using System.Collections.Generic;

namespace Decompiler.Ast.StatementTree
{
    internal class If : Tree
    {
        public readonly AstToken Condition;
        public readonly int EndOffset;
        public Else? ElseTree;
        public List<ElseIf> ElseIfTrees = new();
        public If(Function function, Tree parent, int offset, AstToken condition, int endOffset) : base(function, parent, offset)
        {
            Condition = condition;
            EndOffset = endOffset;
        }

        public override bool IsTreeEnd()
        {
            if (ElseTree != null)
                return true;

            if (Offset < Function.Instructions.Count && Function.Instructions[Offset].Offset >= EndOffset)
                return true;

            if (Statements.Count > 0)
            {
                // TODO: Ast.Return breaks else tree when if statement returns
                return Statements[^1] is Break /*|| Statements[^1] is Ast.Return*/ || Statements[^1] is Jump;
            }

            return false;
        }

        // TODO: wtf did i just write?
        public bool CanSkipBraces()
        {
            if (Statements.Count != 1 || (Statements[0] is not AstToken && (Statements[0] is not If || !(Statements[0] as If).CanSkipBraces())))
                return false;

            if (ElseTree != null && (ElseTree.Statements.Count != 1 || (ElseTree.Statements[0] is not AstToken && (ElseTree.Statements[0] is not If || !(ElseTree.Statements[0] as If).CanSkipBraces()))))
                return false;

            foreach (var elseIf in ElseIfTrees)
            {
                if (elseIf.Statements.Count != 1 || (elseIf.Statements[0] is not AstToken && (elseIf.Statements[0] is not If || !(elseIf.Statements[0] as If).CanSkipBraces())))
                    return false;
            }

            return true;
        }

        public override string ToString()
        {
            string str;

            if (CanSkipBraces())
                str = $"if ({Condition}){Environment.NewLine}{ToString(false)}";
            else
                str = $"if ({Condition}){Environment.NewLine}{{{Environment.NewLine}{base.ToString()}}}";

            foreach (var elseIf in ElseIfTrees)
            {
                str += Environment.NewLine + elseIf.ToString();
            }

            if (ElseTree != null)
                str += Environment.NewLine + ElseTree.ToString();

            return str;
        }
    }
}