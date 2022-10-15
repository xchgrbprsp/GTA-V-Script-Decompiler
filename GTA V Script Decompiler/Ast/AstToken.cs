namespace Decompiler.Ast
{
    internal class AstToken
    {
        public readonly Function function;
        TypeContainer dummyContainer;

        public AstToken(Function func)
        {
            function = func;
        }

        public virtual bool IsStatement()
        {
            return false;
        }

        public virtual bool HasSideEffects()
        {
            return false;
        }

        public virtual int GetStackCount()
        {
            return 1;
        }

        public virtual ref TypeContainer GetTypeContainer()
        {
            if (CanGetGlobalIndex())
                return ref Program.globalTypeMgr.GetGlobalType(GetGlobalIndex());

            dummyContainer = new();
            return ref dummyContainer;
        }

        public virtual void HintType(ref TypeContainer container)
        {
            GetTypeContainer().HintType(ref container);
        }

        public override string ToString()
        {
            return "StackVal";
        }

        public virtual string ToLiteralString()
        {
            return ToString();
        }

        public virtual string ToPointerString()
        {
            return ToString();
        }

        public virtual bool CanGetGlobalIndex()
        {
            return false;
        }

        public virtual int GetGlobalIndex()
        {
            return 0;
        }

        public virtual bool IsPointer()
        {
            return false;
        }
    }
}
