namespace Decompiler.Ast
{
    internal class SecureStore : AstToken
    {
        public readonly AstToken Pointer;
        public readonly AstToken Value;

        public SecureStore(Function func, AstToken pointer, AstToken value) : base(func)
        {
            Pointer = pointer;
            Value = value;
        }

        public override bool IsStatement() => true;

        public override string ToString()
        {
            return $"SECURE_STORE({Pointer}, {Value});";
        }
    }
}
