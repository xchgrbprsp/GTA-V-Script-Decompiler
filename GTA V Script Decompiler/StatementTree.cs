using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler
{
    internal class StatementTree
    {
        public readonly Function Function;
        public List<object> Statements;
        public int Offset;
        public readonly StatementTree? Parent;

        public StatementTree(Function function, StatementTree? parent = null, int offset = 0)
        {
            Function = function;
            Statements = new();
            Offset = offset;
            Parent = parent;
        }

        public virtual bool IsTreeEnd()
        {
            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var statement in Statements)
            {
                if (statement is Ast.AstToken)
                {
                    string repr = statement.ToString();
                    if (!string.IsNullOrEmpty(repr))
                    {
                        sb.Append('\t');
                        sb.AppendLine(statement.ToString());
                    }
                }
                else
                {
                    var lines = statement.ToString().Split(Environment.NewLine);
                    foreach (var line in lines)
                    {
                        if (string.IsNullOrEmpty(line))
                            continue;

                        sb.Append('\t');
                        sb.AppendLine(line);
                    }
                }
            }

            return sb.ToString();
        }
    }

    internal class MainTree : StatementTree
    {
        public MainTree(Function function, StatementTree parent = null, int offset = 0) : base(function, parent, offset)
        {
        }

        public override bool IsTreeEnd()
        {
            if (Statements.Count > 0)
            {
                if (Statements[^1] is Ast.Return)
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            //write all the function variables declared by the function
            if (Properties.Settings.Default.DeclareVariables)
            {
                bool temp = false;
                foreach (string s in Function.Vars.GetDeclaration())
                {
                    sb.Append('\t');
                    sb.AppendLine(s);
                    temp = true;
                }
                if (temp)
                    sb.AppendLine();
            }

            sb.Append(base.ToString());

            return $"{{{Environment.NewLine}{sb.ToString()}}}{Environment.NewLine}";
        }
    }

    internal class SwitchTree : StatementTree
    {
        public readonly Dictionary<int, List<string>> Cases;
        public readonly int BreakOffset;
        public readonly Ast.AstToken SwitchVal;

        public SwitchTree(Function function, StatementTree parent, int offset, Dictionary<int, List<string>> cases, int breakOffset, Ast.AstToken switchVal) : base(function, parent, offset)
        {
            this.Cases = cases;
            this.BreakOffset = breakOffset;
            this.SwitchVal = switchVal;

            foreach (var p in cases)
            {
                Statements.Add(new CaseTree(Function, this, Function.CodeOffsetToFunctionOffset(p.Key), p.Value, breakOffset));
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

    internal class CaseTree : StatementTree
    {
        public readonly int BreakOffset;
        List<string> CaseNames;

        public CaseTree(Function function, StatementTree parent, int offset, List<string> caseNames, int breakOffset) : base(function, parent, offset)
        {
            BreakOffset = breakOffset;
            CaseNames = caseNames;
        }

        public override bool IsTreeEnd()
        {
            if (Offset < Function.Instructions.Count && Function.Instructions[Offset].Offset >= BreakOffset)
                return true;

            if (Statements.Count > 0)
            {
                return Statements[^1] is Ast.Break || Statements[^1] is Ast.Return || Statements[^1] is Ast.Jump;
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (var name in CaseNames)
            {
                if (name != "default")
                    sb.Append("case ");
                sb.AppendLine(name + ":");
            }
            sb.Append(base.ToString());
            return sb.ToString();
        }
    }

    internal class WhileTree : StatementTree
    {
        public readonly int BreakOffset;
        public readonly Ast.AstToken Condition;

        public WhileTree(Function function, StatementTree parent, int offset, Ast.AstToken condition, int breakOffset) : base(function, parent, offset)
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
                return Statements[^1] is Ast.Break /*|| Statements[^1] is Ast.Return*/ || Statements[^1] is Ast.Jump;
            }

            return false;
        }

        public override string ToString()
        {
            return $"while ({Condition.ToString()}){Environment.NewLine}{{{Environment.NewLine}{base.ToString()}}}";
        }
    }

    internal class IfTree : StatementTree
    {
        public readonly Ast.AstToken Condition;
        public readonly int EndOffset;
        public ElseTree? ElseTree;
        public List<ElseIfTree> ElseIfTrees = new();
        public IfTree(Function function, StatementTree parent, int offset, Ast.AstToken condition, int endOffset) : base(function, parent, offset)
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
                return Statements[^1] is Ast.Break /*|| Statements[^1] is Ast.Return*/ || Statements[^1] is Ast.Jump;
            }

            return false;
        }

        public override string ToString()
        {
            var str = $"if ({Condition.ToString()}){Environment.NewLine}{{{Environment.NewLine}{base.ToString()}}}";

            foreach (var elseIf in ElseIfTrees)
            {
                str += Environment.NewLine + elseIf.ToString();
            }

            if (ElseTree != null)
                str += Environment.NewLine + ElseTree.ToString();

            return str;
        }
    }

    internal class ElseIfTree : StatementTree
    {
        public readonly Ast.AstToken Condition;
        public readonly int EndOffset;
        public ElseIfTree(Function function, StatementTree parent, int offset, Ast.AstToken condition, int endOffset) : base(function, parent, offset)
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
            var str = $"else if ({Condition.ToString()}){Environment.NewLine}{{{Environment.NewLine}{base.ToString()}}}";
            return str;
        }
    }

    internal class ElseTree : StatementTree
    {
        public readonly int EndOffset;
        public ElseTree(Function function, StatementTree parent, int offset, int endOffset) : base(function, parent, offset)
        {
            EndOffset = endOffset;
        }

        public override bool IsTreeEnd()
        {
            if (Offset < Function.Instructions.Count && Function.Instructions[Offset].Offset >= EndOffset)
                return true;

            if (Statements.Count > 0)
            {
                return Statements[^1] is Ast.Break /*|| Statements[^1] is Ast.Return*/ || Statements[^1] is Ast.Jump;
            }

            return false;
        }

        public override string ToString()
        {
            return $"else{Environment.NewLine}{{{Environment.NewLine}{base.ToString()}}}";
        }
    }
}
