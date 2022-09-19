using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class IntegerAnd : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerAnd(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        bool IsLogicalOperation()
        {
            return Lhs is not ConstantInt && Rhs is not ConstantInt;
        }

        public override Stack.DataType GetType()
        {
            return IsLogicalOperation() ? Stack.DataType.Bool : Stack.DataType.Int;
        }

        public override string ToString()
        {
            if (IsLogicalOperation())
                return Lhs.ToString() + " && " + Rhs.ToString();
            else
                return Lhs.ToString() + " & " + Rhs.ToString();
        }
    }

    internal class IntegerOr : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerOr(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        bool IsLogicalOperation()
        {
            return Lhs is not ConstantInt && Rhs is not ConstantInt;
        }

        public override Stack.DataType GetType()
        {
            return IsLogicalOperation() ? Stack.DataType.Bool : Stack.DataType.Int;
        }

        public override string ToString()
        {
            if (IsLogicalOperation())
                return Lhs.ToString() + " || " + Rhs.ToString();
            else
                return Lhs.ToString() + " | " + Rhs.ToString();
        }
    }

    internal class IntegerXor : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerXor(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Int;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " ^ " + Rhs.ToString();
        }
    }
}
