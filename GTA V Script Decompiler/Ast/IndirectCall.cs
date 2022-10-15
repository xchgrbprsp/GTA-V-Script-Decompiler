using System.Collections.Generic;

namespace Decompiler.Ast
{
    internal class IndirectCall : FunctionCallBase
    {
        public readonly AstToken Location;

        public IndirectCall(Function func, List<AstToken> arguments, AstToken location) : base(func, arguments)
        {
            Location = location;
            Location.HintType(ref Types.FUNCTION.GetContainer());
        }

        public override string GetName() => Location.ToString();

        public override int GetReturnCount() => 0; // we can't really figure this out afaik
    }
}
