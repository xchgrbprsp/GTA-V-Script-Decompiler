using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Global : AstToken
    {
        readonly public uint Index;

        public Global(Function func, uint index) : base(func)
        {
            Index = index;
        }

        public override string ToString()
        {
            return "&Global_" + Index;
        }

        public override string ToPointerString()
        {
            return "Global_" + Index;
        }
    }

    internal class GlobalLoad : AstToken
    {
        readonly public uint Index;

        public GlobalLoad(Function func, uint index) : base(func)
        {
            Index = index;
        }

        public override Stack.DataType GetType()
        {
            return function.GetFrameVar(Index).DataType;
        }

        public override string ToString()
        {
            return "Global_" + Index;
        }
    }

    internal class GlobalStore : AstToken
    {
        readonly public uint Index;
        readonly public AstToken Value;

        public GlobalStore(Function func, uint index, AstToken value) : base(func)
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
            return "Global_" + Index + " = " + Value.ToString() + ";";
        }
    }
}
