using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class IntegerEq : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerEq(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(Rhs.GetType());
            Rhs.HintType(Lhs.GetType());
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Bool;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " == " + Rhs.ToString();
        }
    }

    internal class IntegerNe : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerNe(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(Rhs.GetType());
            Rhs.HintType(Lhs.GetType());
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Bool;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " != " + Rhs.ToString();
        }
    }

    internal class IntegerLt : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerLt(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Bool;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " < " + Rhs.ToString();
        }
    }

    internal class IntegerLe : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerLe(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Bool;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " <= " + Rhs.ToString();
        }
    }

    internal class IntegerGt : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerGt(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Bool;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " > " + Rhs.ToString();
        }
    }

    internal class IntegerGe : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerGe(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Bool;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " > " + Rhs.ToString();
        }
    }
}
