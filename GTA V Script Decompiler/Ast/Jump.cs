namespace Decompiler.Ast
{
    /// <summary>
    /// Placeholder for unhandled jumps
    /// </summary>
    internal class Jump : AstToken
    {
        public readonly int Offset;
        public Jump(Function func, int offset) : base(func) => Offset = offset;

        public override bool IsStatement() => true;

        /// <summary>
        /// Should ideally not be called
        /// </summary>
        public override string ToString() => "goto 0x" + Offset.ToString("X") + ";";
    }
}
