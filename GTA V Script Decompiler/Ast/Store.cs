namespace Decompiler.Ast
{
    internal class Store : AstToken
    {
        public readonly AstToken Pointer;
        public readonly AstToken Value;

        public Store(Function func, AstToken pointer, AstToken value) : base(func)
        {
            Pointer = pointer;
            Value = value;

            //if (Types.HasPointerVersion(value.GetType()))
            //    Pointer.HintType(Types.GetPointerVersion(value.GetType()));
        }

        public override bool IsStatement() => true;

        public override string ToString()
        {
            return Pointer.IsPointer()
                ? Pointer.ToPointerString() + " = " + Value.ToString() + ";"
                : "*" + Pointer.ToPointerString() + " = " + Value.ToString() + ";";
        }
    }
}
