namespace Decompiler.Ast
{
    internal class IntegerAnd : AstToken
    {
        private readonly AstToken Lhs;
        private readonly AstToken Rhs;
        public IntegerAnd(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Rhs.GetTypeContainer());
        }

        private bool IsLogicalOperation() => Lhs is not ConstantInt && Rhs is not ConstantInt;

        public override ref TypeContainer GetTypeContainer()
        {
            var type = IsLogicalOperation() ? ref Types.BOOL : ref Types.INT;
            return ref type.GetContainer();
        }

        public override string ToString() => IsLogicalOperation() ? Lhs.ToString() + " && " + Rhs.ToString() : Lhs.ToString() + " & " + Rhs.ToString();
    }

    internal class IntegerOr : AstToken
    {
        private readonly AstToken Lhs;
        private readonly AstToken Rhs;
        public IntegerOr(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Rhs.GetTypeContainer());
        }

        private bool IsLogicalOperation() => Lhs is not ConstantInt && Rhs is not ConstantInt;

        public override ref TypeContainer GetTypeContainer()
        {
            var type = IsLogicalOperation() ? ref Types.BOOL : ref Types.INT;
            return ref type.GetContainer();
        }

        public override string ToString() => IsLogicalOperation() ? Lhs.ToString() + " || " + Rhs.ToString() : Lhs.ToString() + " | " + Rhs.ToString();
    }

    internal class IntegerXor : AstToken
    {
        private readonly AstToken Lhs;
        private readonly AstToken Rhs;
        public IntegerXor(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override ref TypeContainer GetTypeContainer() => ref Types.INT.GetContainer();

        public override string ToString() => Lhs.ToString() + " ^ " + Rhs.ToString();
    }
}
