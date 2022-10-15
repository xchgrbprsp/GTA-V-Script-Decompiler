namespace Decompiler.Ast
{
    internal class IntToFloat : AstToken
    {
        readonly AstToken Integer;
        public IntToFloat(Function func, AstToken integer) : base(func)
        {
            Integer = integer;
            Integer.HintType(ref Types.INT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.FLOAT.GetContainer();
        }

        public override string ToString()
        {
            return "(float)" + Integer.ToString();
        }
    }
}
