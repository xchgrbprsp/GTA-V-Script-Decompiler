namespace Decompiler.Ast
{
    internal class Local : AstToken
    {
        public readonly uint Index;

        public Local(Function func, uint index) : base(func)
        {
            Index = index;
            function.GetFrameVar(Index).SetCalled();
        }

        public override string ToString() => "&" + function.GetFrameVarName(Index);

        public override string ToPointerString() => function.GetFrameVarName(Index);

        public override bool IsPointer() => true;

#if false
        public override void HintType(Stack.DataType type)
        {
            if (Types.HasLiteralVersion(type))
                function.GetFrameVar(Index).HintType(Types.GetLiteralVersion(type));
        }
#endif
    }

    internal class LocalLoad : AstToken
    {
        public readonly uint Index;

        public LocalLoad(Function func, uint index) : base(func)
        {
            Index = index;
            function.GetFrameVar(Index).SetCalled();
        }

        public override ref TypeContainer GetTypeContainer() => ref function.GetFrameVar(Index).DataType;

        public override string ToString() => function.GetFrameVarName(Index);

        public override void HintType(ref TypeContainer container) => function.GetFrameVar(Index).HintType(ref container);
    }

    internal class LocalStore : AstToken
    {
        public readonly uint Index;
        public readonly AstToken Value;

        public LocalStore(Function func, uint index, AstToken value) : base(func)
        {
            Index = index;
            Value = value;
            function.GetFrameVar(Index).SetCalled();
            function.GetFrameVar(Index).HintType(ref Value.GetTypeContainer());

            // I really have to move this somewhere else

            if (value is NativeCall)
            {
                var entry = (value as NativeCall).Entry;
                if (entry != null)
                {
                    if (NativeReturnAutoName.CanApply(entry.Value))
                        function.SetFrameVarAutoName(index, new NativeReturnAutoName(entry.Value));
                }
            }
        }

        public override bool IsStatement() => true;

        public override string ToString() => function.GetFrameVarName(Index) + " = " + Value.ToString() + ";";
    }
}
