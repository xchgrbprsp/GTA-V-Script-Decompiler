namespace Decompiler.Ast
{
    internal class ConstantFloat : AstToken
    {
        private readonly float Value;

        public ConstantFloat(Function func, float value) : base(func) => Value = value;

        public override ref TypeContainer GetTypeContainer() => ref Types.FLOAT.GetContainer();

        public override string ToString() => Value.ToString() + "f";
    }
}
