namespace Decompiler.Ast
{
    internal class IntToFloat : AstToken
    {
        private readonly AstToken Integer;
        public IntToFloat(Function func, AstToken integer) : base(func)
        {
            Integer = integer;
            Integer.HintType(ref Types.INT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer() => ref Types.FLOAT.GetContainer();

        public override string ToString() => "(float)" + Integer.ToString();
    }
}
