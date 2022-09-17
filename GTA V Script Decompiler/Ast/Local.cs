using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Local : AstToken
    {
        readonly public uint Index;

        public Local(Function func, uint index) : base(func)
        {
            Index = index;
        }

        public override string ToString()
        {
            return "&" + function.GetFrameVarName(Index);
        }

        public override string ToPointerString()
        {
            return function.GetFrameVarName(Index);
        }
    }

    internal class LocalLoad : AstToken
    {
        readonly public uint Index;

        public LocalLoad(Function func, uint index) : base(func)
        {
            Index = index;
        }

        public override Stack.DataType GetType()
        {
            return function.GetFrameVar(Index).DataType;
        }

        public override string ToString()
        {
            return function.GetFrameVarName(Index);
        }
    }

    internal class LocalStore : AstToken
    {
        readonly public uint Index;
        readonly public AstToken Value;

        public LocalStore(Function func, uint index, AstToken value) : base(func)
        {
            Index = index;
            Value = value;
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            return function.GetFrameVarName(Index) + " = " + Value.ToString() + ";";
        }
    }
}
