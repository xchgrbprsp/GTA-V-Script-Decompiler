namespace Decompiler.Ast
{
    internal class Drop : AstToken
    {
        private readonly AstToken Dropped;
        public Drop(Function func, AstToken dropped) : base(func) => Dropped = dropped;

        public override bool IsStatement() => true;

        public override string ToString() => Dropped.ToString() + ";";
    }
}
