using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast.StatementTree
{
    internal class Switch : Tree
    {
        public readonly Dictionary<int, List<AstToken>> Cases;
        public readonly int BreakOffset;
        public readonly AstToken SwitchVal;

        public Switch(Function function, Tree parent, int offset, Dictionary<int, List<AstToken>> cases, int breakOffset, AstToken switchVal) : base(function, parent, offset)
        {
            this.Cases = cases;
            this.BreakOffset = breakOffset;
            this.SwitchVal = switchVal;

            foreach (var p in cases)
            {
                foreach (var @case in p.Value)
                {
                    @case.HintType(ref SwitchVal.GetTypeContainer());
                }

                Statements.Add(new Case(Function, this, Function.CodeOffsetToFunctionOffset(p.Key), p.Value, breakOffset));
            }
        }

        /// <returns>Isn't going to be called anyway</returns>
        public override bool IsTreeEnd()
        {
            return true;
        }

        public override string ToString()
        {
            return $"switch ({SwitchVal.ToString()}){Environment.NewLine}{{{Environment.NewLine}{base.ToString()}}}";
        }
    }
}
