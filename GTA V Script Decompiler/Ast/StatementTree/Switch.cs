using System;
using System.Collections.Generic;

namespace Decompiler.Ast.StatementTree
{
    internal class Switch : Tree
    {
        public readonly Dictionary<int, List<AstToken>> Cases;
        public readonly int BreakOffset;
        public readonly AstToken SwitchVal;

        public Switch(Function function, Tree parent, int offset, Dictionary<int, List<AstToken>> cases, int breakOffset, AstToken switchVal) : base(function, parent, offset)
        {
            Cases = cases;
            BreakOffset = breakOffset;
            SwitchVal = switchVal;

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
        public override bool IsTreeEnd() => true;

        public override string ToString() => $"switch ({SwitchVal}){Environment.NewLine}{{{Environment.NewLine}{base.ToString()}}}";
    }
}
