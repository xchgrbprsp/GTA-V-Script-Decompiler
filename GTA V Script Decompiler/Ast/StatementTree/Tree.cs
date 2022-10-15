using System;
using System.Collections.Generic;
using System.Text;

namespace Decompiler.Ast.StatementTree
{
    internal class Tree
    {
        public readonly Function Function;
        public List<object> Statements;
        public int Offset;
        public Tree? Parent;

        public Tree(Function function, Tree? parent = null, int offset = 0)
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

        public string ToString(bool newlines)
        {
            StringBuilder sb = new();
            var lastIsTree = false;
            var first = true;

            foreach (var statement in Statements)
            {
                if (statement is AstToken)
                {
                    if (lastIsTree && (this is not Case || statement is not Break))
                        sb.AppendLine();

                    var repr = statement.ToString();
                    if (!string.IsNullOrEmpty(repr))
                    {
                        sb.Append('\t');
                        sb.AppendLine(statement.ToString());
                    }
                }
                else
                {
                    if (!first)
                        sb.AppendLine();

                    var lines = statement.ToString().Split(Environment.NewLine);
                    foreach (var line in lines)
                    {
                        //if (string.IsNullOrEmpty(line))
                        //    continue;

                        sb.Append('\t');
                        sb.AppendLine(line);
                    }
                }

                lastIsTree = statement is Tree;
                first = false;
            }

            var str = sb.ToString();

            if (!newlines)
                str = str.TrimEnd();

            return str;
        }

        public override string ToString()
        {
            return ToString(true);
        }
    }
}
