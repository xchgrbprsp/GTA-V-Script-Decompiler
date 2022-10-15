namespace Decompiler.Ast
{
    internal class FloatToInt : AstToken
    {
        readonly AstToken Float;
        public FloatToInt(Function func, AstToken @float) : base(func)
        {
            Float = @float;
            Float.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.INT.GetContainer();
        }

        public override string ToString()
        {
            return "(int)" + Float.ToString();
        }
    }
}
