namespace Decompiler.Ast
{
    internal class SecureLoad : AstToken
    {
        public readonly AstToken UnderlyingToken;

        public SecureLoad(Function func, AstToken underlyingToken) : base(func)
        {
            UnderlyingToken = underlyingToken;
        }

        public override ref TypeContainer GetTypeContainer() => ref UnderlyingToken.GetTypeContainer();

        public override void HintType(ref TypeContainer container)
        {
            UnderlyingToken.HintType(ref container);
        }

        public override string ToString() => $"SECURE_LOAD({UnderlyingToken})";
    }
}
