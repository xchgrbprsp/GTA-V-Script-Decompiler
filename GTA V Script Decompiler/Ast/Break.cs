namespace Decompiler.Ast
{
    internal class Break : AstToken
    {
        public Break(Function func) : base(func)
        {
        }

        public override bool IsStatement() => true;

        public override string ToString() => "break;";
    }
}
