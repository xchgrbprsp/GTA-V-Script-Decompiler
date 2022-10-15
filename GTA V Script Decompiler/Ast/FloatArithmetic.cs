namespace Decompiler.Ast
{
    internal abstract class FloatArithmetic : AstToken
    {
        readonly AstToken Lhs;
        readonly AstToken Rhs;
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
            return operand is not FloatArithmetic ? false : this.GetType() != operand.GetType();
        }

        public override string ToString()
        {
            var lhs = IsComplexOperand(Lhs) ? "(" + Lhs.ToString() + ")" : Lhs.ToString();
            var rhs = IsComplexOperand(Rhs) ? "(" + Rhs.ToString() + ")" : Rhs.ToString();

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
        readonly AstToken value;
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
            return value is FloatArithmetic ? $"-({value})" : $"-{value}";
        }
    }
}
