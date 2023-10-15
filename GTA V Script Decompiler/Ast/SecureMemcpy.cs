namespace Decompiler.Ast
{
    internal class SecureMemcpy : AstToken
    {
        public readonly AstToken Pointer;
        public readonly AstToken Value;

        public SecureMemcpy(Function func, AstToken pointer, AstToken value) : base(func)
        {
            Pointer = pointer;
            Value = value;
        }

        public override bool IsStatement() => true;

        public override string ToString()
        {
            return $"SECURE_MEMCPY({Pointer}, {Value}, SIZE_OF({Pointer.ToPointerString()}));";
        }
    }
}
