namespace Decompiler.Ast
{
    internal class AstToken
    {
        public readonly Function function;
        private TypeContainer dummyContainer;

        public AstToken(Function func) => function = func;

        public virtual bool IsStatement() => false;

        public virtual bool HasSideEffects() => false;

        public virtual int GetStackCount() => 1;

        public virtual ref TypeContainer GetTypeContainer()
        {
            if (CanGetGlobalIndex())
                return ref Program.GlobalTypeMgr.GetGlobalType(GetGlobalIndex());

            dummyContainer = new();
            return ref dummyContainer;
        }

        public virtual void HintType(ref TypeContainer container) => GetTypeContainer().HintType(ref container);

        public override string ToString() => "StackVal";

        public virtual string ToLiteralString() => ToString();

        public virtual string ToPointerString() => ToString();

        public virtual bool CanGetGlobalIndex() => false;

        public virtual int GetGlobalIndex() => 0;

        public virtual bool IsPointer() => false;
    }
}
