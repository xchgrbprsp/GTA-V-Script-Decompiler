namespace Decompiler
{
    public class Variable
    {
        public bool IsStruct;
        public TypeContainer DataType;
        public int Index { get; private set; }
        public long Value { get; set; }
        public int ImmediateSize { get; set; }
        public string Name { get; set; } = "";
        public bool Is_Used { get; private set; }
        public bool Is_Called { get; private set; } = false;
        public bool Is_Array { get; private set; }

        internal AutoName AutoName { get; private set; }

        public Variable(int index)
        {
            Index = index;
            Value = 0;
            ImmediateSize = 1;
            Is_Array = false;
            Is_Used = true;
            DataType = new(Types.UNKNOWN);
            AutoName = new DefaultAutoName(this);
        }

        public Variable(int index, long value)
        {
            Index = index;
            Value = value;
            ImmediateSize = 1;
            Is_Array = false;
            Is_Used = true;
            DataType = new(Types.UNKNOWN);
            IsStruct = false;
            AutoName = new DefaultAutoName(this);
        }

        public void SetArray()
        {
            if (!IsStruct)
                Is_Array = true;
        }

        public void SetCalled() => Is_Called = true;

        public void SetStruct()
        {
            DataType = new(Types.UNKNOWN);
            Is_Array = false;
            IsStruct = true;
        }

        public void SetNotUsed() => Is_Used = false;

        internal void SetAutoName(AutoName autoName)
        {
            if (autoName.GetPrecedence() > AutoName.GetPrecedence())
                AutoName = autoName;
        }

        public void HintType(ref TypeContainer container) => DataType.HintType(ref container);

        public ref TypeContainer GetTypeContainer() => ref DataType;
    }
}
