using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast.StatementTree
{
    internal class Main : Tree
    {
        public Main(Function function, Tree parent = null, int offset = 0) : base(function, parent, offset)
        {
        }

        public override bool IsTreeEnd()
        {
            if (Statements.Count > 0)
            {
                if (Statements[^1] is Return)
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
}
