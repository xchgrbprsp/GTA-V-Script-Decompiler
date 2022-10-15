namespace Decompiler.Ast
{
    internal class FloatToInt : AstToken
    {
        private readonly AstToken Float;
        public FloatToInt(Function func, AstToken @float) : base(func)
        {
            Float = @float;
            Float.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer() => ref Types.INT.GetContainer();

        public override string ToString() => "(int)" + Float.ToString();
    }
}
