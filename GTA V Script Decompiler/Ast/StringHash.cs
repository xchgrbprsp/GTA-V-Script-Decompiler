namespace Decompiler.Ast
{
    /// <summary>
    /// Unused by game scripts
    /// </summary>
    internal class StringHash : AstToken
    {
        public readonly AstToken @string;

        public StringHash(Function func, AstToken @string) : base(func)
        {
            this.@string = @string;
            @string.HintType(ref Types.PSTRING.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer() => ref Types.HASH.GetContainer();

        public override string ToString() => "joaat(" + @string.ToString() + ")";
    }
}
