using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal abstract class FloatArithmetic : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        protected abstract char Symbol { get; }

        protected FloatArithmetic(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.FLOAT.GetContainer();
        }

        public bool IsComplexOperand(AstToken operand)
        {
            if (operand is not FloatArithmetic)
                return false;
            else
                return this.GetType() != operand.GetType();
        }

        public override string ToString()
        {
            string lhs = IsComplexOperand(Lhs) ? "(" + Lhs.ToString() + ")" : Lhs.ToString();
            string rhs = IsComplexOperand(Rhs) ? "(" + Rhs.ToString() + ")" : Rhs.ToString();

            return $"{lhs} {Symbol} {rhs}";
        }
    }

    internal class FloatAdd : FloatArithmetic
    {
        public FloatAdd(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '+';
    }

    internal class FloatSub : FloatArithmetic
    {
        public FloatSub(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '-';
    }

    internal class FloatMul : FloatArithmetic
    {
        public FloatMul(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '*';
    }

    internal class FloatDiv : FloatArithmetic
    {
        public FloatDiv(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '/';
    }

    internal class FloatMod : FloatArithmetic
    {
        public FloatMod(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '%';
    }

    internal class FloatNeg : AstToken
    {
        AstToken value;
        public FloatNeg(Function func, AstToken value) : base(func)
        {
            this.value = value;
            this.value.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.FLOAT.GetContainer();
        }

        public override string ToString()
        {
            if (value is FloatArithmetic)
                return $"-({value})";
            else
                return $"-{value}";
        }
    }
}
