namespace Decompiler.Ast
{
    internal abstract class VectorArithmetic : AstToken
    {
        private readonly AstToken Lhs;
        private readonly AstToken Rhs;
        protected abstract char Symbol { get; }

        protected VectorArithmetic(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.VEC3.GetContainer());
            Rhs.HintType(ref Types.VEC3.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer() => ref Types.VEC3.GetContainer();

        public override int GetStackCount() => 3;

        public bool IsComplexOperand(AstToken operand) => operand is VectorArithmetic && GetType() != operand.GetType();

        public override string ToString()
        {
            var lhs = IsComplexOperand(Lhs) ? "(" + Lhs.ToString() + ")" : Lhs.ToString();
            var rhs = IsComplexOperand(Rhs) ? "(" + Rhs.ToString() + ")" : Rhs.ToString();

            return $"{lhs} {Symbol} {rhs}";
        }
    }

    internal class VectorAdd : VectorArithmetic
    {
        public VectorAdd(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '+';
    }

    internal class VectorSub : VectorArithmetic
    {
        public VectorSub(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '-';
    }

    internal class VectorMul : VectorArithmetic
    {
        public VectorMul(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '*';
    }

    internal class VectorDiv : VectorArithmetic
    {
        public VectorDiv(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '/';
    }

    internal class VectorNeg : AstToken
    {
        private readonly AstToken value;
        public VectorNeg(Function func, AstToken value) : base(func)
        {
            this.value = value;
            this.value.HintType(ref Types.INT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer() => ref Types.VEC3.GetContainer();

        public override int GetStackCount() => 3;

        public override string ToString() => value is VectorArithmetic ? $"-({value})" : $"-{value}";
    }
}
