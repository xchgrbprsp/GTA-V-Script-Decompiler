namespace Decompiler.Ast
{
    internal class Static : AstToken
    {
        public readonly uint Index;

        public Static(Function func, uint index) : base(func) => Index = index;

        public override string ToString() => "&" + function.ScriptFile.Statics.GetVarName(Index);

        public override string ToPointerString() => function.ScriptFile.Statics.GetVarName(Index);

        public override bool IsPointer() => true;
    }

    internal class StaticLoad : AstToken
    {
        public readonly uint Index;

        public StaticLoad(Function func, uint index) : base(func) => Index = index;

        public override ref TypeContainer GetTypeContainer() => ref function.ScriptFile.Statics.GetVarAtIndex(Index).DataType;

        public override void HintType(ref TypeContainer container) => function.ScriptFile.Statics.GetVarAtIndex(Index).HintType(ref container);

        public override string ToString() => function.ScriptFile.Statics.GetVarName(Index);
    }

    internal class StaticStore : AstToken
    {
        public readonly uint Index;
        public readonly AstToken Value;

        public StaticStore(Function func, uint index, AstToken value) : base(func)
        {
            Index = index;
            Value = value;

            function.ScriptFile.Statics.GetVarAtIndex(Index).HintType(ref Value.GetTypeContainer());
        }

        public override bool IsStatement() => true;

        public override string ToString() => function.ScriptFile.Statics.GetVarName(Index) + " = " + Value.ToString() + ";";
    }
}
