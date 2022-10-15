namespace Decompiler.Ast
{
    internal class Global : AstToken
    {
        public readonly uint Index;

        public Global(Function func, uint index) : base(func)
        {
            Index = index;
        }

        public override string ToString()
        {
            return "&Global_" + Index;
        }

        public override string ToPointerString()
        {
            return "Global_" + Index;
        }

        public override bool CanGetGlobalIndex()
        {
            return true;
        }

        public override int GetGlobalIndex()
        {
            return (int)Index;
        }

        public override bool IsPointer()
        {
            return true;
        }
    }

    internal class GlobalLoad : AstToken
    {
        public readonly uint Index;

        public GlobalLoad(Function func, uint index) : base(func)
        {
            Index = index;
        }

        public override string ToString()
        {
            return "Global_" + Index;
        }

        public override bool CanGetGlobalIndex()
        {
            return true;
        }

        public override int GetGlobalIndex()
        {
            return (int)Index;
        }
    }

    internal class GlobalStore : AstToken
    {
        public readonly uint Index;
        public readonly AstToken Value;

        public GlobalStore(Function func, uint index, AstToken value) : base(func)
        {
            Index = index;
            Value = value;

            HintType(ref value.GetTypeContainer());
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            return "Global_" + Index + " = " + Value.ToString() + ";";
        }

        public override bool CanGetGlobalIndex()
        {
            return true;
        }

        public override int GetGlobalIndex()
        {
            return (int)Index;
        }
    }
}
