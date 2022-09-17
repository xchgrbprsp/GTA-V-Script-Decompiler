using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Return : AstToken
    {
        public readonly List<AstToken> ReturnValues;
        public Return(Function func, List<AstToken> returnValues) : base(func)
        {
            ReturnValues = returnValues;
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            if (ReturnValues.Count == 0)
            {
                return "return;";
            }
            else
            {
                bool first = true;
                StringBuilder sb = new();

                foreach (var token in ReturnValues)
                {
                    if (!first)
                        sb.Append(", ");
                    sb.Append(token.ToString());
                    first = false;
                }

                sb.Append(';');

                return sb.ToString();
            }
        }
    }
}
