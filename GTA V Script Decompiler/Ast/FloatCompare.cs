namespace Decompiler.Ast
{
    internal abstract class FloatCompare : AstToken
    {
        readonly AstToken Lhs;
        readonly AstToken Rhs;

        protected FloatCompare(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.FLOAT.GetContainer());
            Rhs.HintType(ref Types.FLOAT.GetContainer());
        }

        protected abstract string Operator { get; }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.BOOL.GetContainer();
        }

        public override string ToString()
        {
            return $"{Lhs} {Operator} {Rhs}";
        }
    }

    internal class FloatEq : FloatCompare
    {
        public FloatEq(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override string Operator => "==";
    }

    internal class FloatNe : FloatCompare
    {
        public FloatNe(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override string Operator => "!=";
    }

    internal class FloatLt : FloatCompare
    {
        public FloatLt(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override string Operator => "<";
    }

    internal class FloatLe : FloatCompare
    {
        public FloatLe(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override string Operator => "<=";
    }

    internal class FloatGt : FloatCompare
    {
        public FloatGt(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override string Operator => ">";
    }

    internal class FloatGe : FloatCompare
    {
        public FloatGe(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override string Operator => ">=";
    }
}
