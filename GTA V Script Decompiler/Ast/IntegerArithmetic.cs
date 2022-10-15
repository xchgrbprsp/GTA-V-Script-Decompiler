namespace Decompiler.Ast
{
    internal abstract class IntegerArithmetic : AstToken
    {
        readonly AstToken Lhs;
        readonly AstToken Rhs;
        protected abstract char Symbol { get; }

        protected IntegerArithmetic(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.INT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.INT.GetContainer();
        }

        public bool IsComplexOperand(AstToken operand)
        {
            if (operand is not IntegerArithmetic)
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

    internal class IntegerAdd : IntegerArithmetic
    {
        public IntegerAdd(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '+';
    }

    internal class IntegerSub : IntegerArithmetic
    {
        public IntegerSub(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '-';
    }

    internal class IntegerMul : IntegerArithmetic
    {
        public IntegerMul(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '*';
    }

    internal class IntegerDiv : IntegerArithmetic
    {
        public IntegerDiv(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '/';
    }

    internal class IntegerMod : IntegerArithmetic
    {
        public IntegerMod(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '%';
    }

    internal class IntegerNeg : AstToken
    {
        readonly AstToken value;
        public IntegerNeg(Function func, AstToken value) : base(func)
        {
            this.value = value;
            this.value.HintType(ref Types.INT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.INT.GetContainer();
        }

        public override string ToString()
        {
            if (value is IntegerArithmetic)
                return $"-({value})";
            else
                return $"-{value}";
        }
    }

    internal class IntegerNot : AstToken
    {
        readonly AstToken value;
        public IntegerNot(Function func, AstToken value) : base(func)
        {
            this.value = value;
            this.value.HintType(ref Types.BOOL.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.BOOL.GetContainer();
        }

        public override string ToString()
        {
            if (value is IntegerAnd || value is IntegerOr)
                return $"!({value})";
            else
                return $"!{value}";
        }
    }
}
