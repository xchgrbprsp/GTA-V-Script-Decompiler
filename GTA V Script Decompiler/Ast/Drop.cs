namespace Decompiler.Ast
{
    internal class Drop : AstToken
    {
        readonly AstToken Dropped;
        public Drop(Function func, AstToken dropped) : base(func)
        {
            Dropped = dropped;
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            return Dropped.ToString() + ";";
        }
    }
}
