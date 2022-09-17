using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class FloatEq : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatEq(Function func, AstToken lhs, AstToken rhs) : base(func)
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
            return Lhs + " == " + Rhs;
        }
    }

    internal class FloatNe : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatNe(Function func, AstToken lhs, AstToken rhs) : base(func)
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
            return Lhs + " != " + Rhs;
        }
    }

    internal class FloatLt : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatLt(Function func, AstToken lhs, AstToken rhs) : base(func)
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
            return Lhs + " < " + Rhs;
        }
    }

    internal class FloatLe : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatLe(Function func, AstToken lhs, AstToken rhs) : base(func)
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
            return Lhs + " <= " + Rhs;
        }
    }

    internal class FloatGt : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatGt(Function func, AstToken lhs, AstToken rhs) : base(func)
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
            return Lhs + " > " + Rhs;
        }
    }

    internal class FloatGe : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatGe(Function func, AstToken lhs, AstToken rhs) : base(func)
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
            return Lhs + " > " + Rhs;
        }
    }
}
