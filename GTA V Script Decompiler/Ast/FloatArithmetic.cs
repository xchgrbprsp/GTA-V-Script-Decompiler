namespace Decompiler.Ast
{
    internal abstract class FloatArithmetic : AstToken
    {
        private readonly AstToken Lhs;
        private readonly AstToken Rhs;
        protected abstract char Symbol { get; }

        protected FloatArithmetic(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer() => ref Types.FLOAT.GetContainer();

        public bool IsComplexOperand(AstToken operand) => operand is FloatArithmetic && GetType() != operand.GetType();

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
        private readonly AstToken value;
        public FloatNeg(Function func, AstToken value) : base(func)
        {
            this.value = value;
            this.value.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer() => ref Types.FLOAT.GetContainer();

        public override string ToString() => value is FloatArithmetic ? $"-({value})" : $"-{value}";
    }
}
