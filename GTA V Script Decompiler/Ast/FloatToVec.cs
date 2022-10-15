namespace Decompiler.Ast
{
    /// <summary>
    /// Required because it is incorrect to dup values with side effects
    /// </summary>
    internal class FloatToVec : AstToken
    {
        public readonly AstToken Float;
        public FloatToVec(Function func, AstToken @float) : base(func)
        {
            Float = @float;

            Float.HintType(ref Types.FLOAT.GetContainer());
        }

        public override int GetStackCount() => 3;

        public override ref TypeContainer GetTypeContainer() => ref Types.VEC3.GetContainer();

        public override string ToString() => "F2V(" + Float.ToString() + ")";
    }
}
