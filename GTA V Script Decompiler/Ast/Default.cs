namespace Decompiler.Ast
{
    internal class Default : AstToken
    {
        public Default(Function func) : base(func)
        {
        }

        public override string ToString()
        {
            return "default";
        }
    }
}
