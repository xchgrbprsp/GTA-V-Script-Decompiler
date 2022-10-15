namespace Decompiler.Ast
{
    internal class Load : AstToken
    {
        private readonly AstToken Pointer;
        public Load(Function func, AstToken pointer) : base(func) => Pointer = pointer;

        public override string ToString() => "*" + Pointer.ToPointerString();

#if false
        public override void HintType(Stack.DataType type)
        {
            if (Types.HasPointerVersion(type))
                Pointer.HintType(Types.GetPointerVersion(type));
        }
#endif
    }
}
