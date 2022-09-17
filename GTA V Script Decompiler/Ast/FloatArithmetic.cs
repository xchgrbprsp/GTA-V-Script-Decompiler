using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class FloatAdd : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatAdd(Function func, AstToken lhs, AstToken rhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Float;
        }

        public override string ToString()
        {
            return Lhs + " + " + Rhs;
        }
    }

    internal class FloatDiv : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatDiv(Function func, AstToken lhs, AstToken rhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Float;
        }

        public override string ToString()
        {
            return Lhs + " / " + Rhs;
        }
    }

    internal class FloatMod : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatMod(Function func, AstToken lhs, AstToken rhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Float;
        }

        public override string ToString()
        {
            return Lhs + " % " + Rhs;
        }
    }

    internal class FloatMul : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatMul(Function func, AstToken lhs, AstToken rhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Float;
        }

        public override string ToString()
        {
            return Lhs + " * " + Rhs;
        }
    }

    internal class FloatNeg : AstToken
    {
        AstToken value;
        public FloatNeg(Function func, AstToken value) : base(func)
        {
            this.value = value;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Float;
        }

        public override string ToString()
        {
            return "-" + value;
        }
    }

    internal class FloatSub : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatSub(Function func, AstToken lhs, AstToken rhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Float;
        }

        public override string ToString()
        {
            return Lhs + " - " + Rhs;
        }
    }
}
