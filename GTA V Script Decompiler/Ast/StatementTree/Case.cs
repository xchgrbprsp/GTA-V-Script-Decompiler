using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast.StatementTree
{
    internal class Case : Tree
    {
        public readonly int BreakOffset;
        readonly int StartOffset;
        List<AstToken> Cases;

        public Case(Function function, Tree parent, int offset, List<AstToken> cases, int breakOffset) : base(function, parent, offset)
        {
            StartOffset = offset;
            BreakOffset = breakOffset;
            Cases = cases;
        }

        public override bool IsTreeEnd()
        {
            if (Offset < Function.Instructions.Count && Function.Instructions[Offset].Offset >= BreakOffset)
                return true;

            if (Statements.Count > 0 && (Statements[^1] is Break || Statements[^1] is Return || Statements[^1] is Jump))
            {
                return true;
            }

            foreach (var p in (Parent as Switch).Cases)
            {
                if (Function.CodeOffsetToFunctionOffset(p.Key) != StartOffset && Function.CodeOffsetToFunctionOffset(p.Key) == Offset)
                {
                    Statements.Add(new Attribute(Function, "fallthrough"));
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (var @case in Cases)
            {
                if (@case is not Ast.Default)
                    sb.Append("case ");
                sb.AppendLine(@case.ToString() + ":");
            }
            sb.Append(base.ToString(false));
            return sb.ToString();
        }
    }
}
