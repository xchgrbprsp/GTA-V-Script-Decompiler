namespace Decompiler.Ast
{
    internal class ConstantFloat : AstToken
    {
        readonly float Value;

        public ConstantFloat(Function func, float value) : base(func)
        {
            Value = value;
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.FLOAT.GetContainer();
        }

        public override string ToString()
        {
            return Value.ToString() + "f";
        }
    }
}
