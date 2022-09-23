using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class StoreN : AstToken
    {
        readonly AstToken Pointer;
        readonly AstToken Count;
        readonly List<AstToken> Values;

        public StoreN(Function func, AstToken pointer, AstToken count, List<AstToken> values) : base(func)
        {
            Pointer = pointer;
            Count = count;
            Values = values;
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            string res = Pointer.ToPointerString() + " = { ";
            foreach (var value in Values)
                res += value + ", ";
            return res.Remove(res.Length - 2) + " };";
        }
    }
}
