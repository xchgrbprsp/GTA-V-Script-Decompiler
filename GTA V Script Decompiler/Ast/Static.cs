using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Static : AstToken
    {
        public readonly uint Index;

        public Static(Function func, uint index) : base(func)
        {
            Index = index;
        }

        public override string ToString()
        {
            return "&" + function.Scriptfile.Statics.GetVarName(Index);
        }

        public override string ToPointerString()
        {
            return function.Scriptfile.Statics.GetVarName(Index);
        }

        public override bool IsPointer()
        {
            return true;
        }
    }
    
    internal class StaticLoad : AstToken
    {
        public readonly uint Index;

        public StaticLoad(Function func, uint index) : base(func)
        {
            Index = index;
        }

        public override Stack.DataType GetType()
        {
            return function.Scriptfile.Statics.GetVarAtIndex(Index).DataType;
        }

        public override void HintType(Stack.DataType type)
        {
            function.Scriptfile.Statics.GetVarAtIndex(Index).HintType(type);
        }

        public override string ToString()
        {
            return function.Scriptfile.Statics.GetVarName(Index);
        }
    }

    internal class StaticStore : AstToken
    {
        public readonly uint Index;
        public readonly AstToken Value;

        public StaticStore(Function func, uint index, AstToken value) : base(func)
        {
            Index = index;
            Value = value;

            function.Scriptfile.Statics.GetVarAtIndex(Index).HintType(Value.GetType());
            Value.HintType(function.Scriptfile.Statics.GetVarAtIndex(Index).DataType);
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            return function.Scriptfile.Statics.GetVarName(Index) + " = " + Value.ToString() + ";";
        }
    }
}
