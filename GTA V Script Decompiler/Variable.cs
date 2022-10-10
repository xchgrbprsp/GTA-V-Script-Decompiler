using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler
{
    public class Variable
    {
        bool IsArray;
        bool IsUsed;
        public bool IsStruct;
        bool IsCalled = false;
        public TypeContainer DataType;
        public int Index { get; private set; }
        public long Value { get; set; }
        public int ImmediateSize { get; set; }
        public string Name { get; set; } = "";
        public bool Is_Used { get { return IsUsed; } }
        public bool Is_Called { get { return IsCalled; } }
        public bool Is_Array { get { return IsArray; } }

        internal AutoName AutoName { get; private set; }

        public Variable(int index)
        {
            Index = index;
            Value = 0;
            ImmediateSize = 1;
            IsArray = false;
            IsUsed = true;
            DataType = new(Types.UNKNOWN);
            AutoName = new DefaultAutoName(this);
        }

        public Variable(int index, long value)
        {
            Index = index;
            Value = value;
            ImmediateSize = 1;
            IsArray = false;
            IsUsed = true;
            DataType = new(Types.UNKNOWN);
            IsStruct = false;
            AutoName = new DefaultAutoName(this);
        }

        public void SetArray()
        {
            if (!IsStruct)
                IsArray = true;
        }

        public void SetCalled()
        {
            IsCalled = true;
        }

        public void SetStruct()
        {
            DataType = new(Types.UNKNOWN);
            IsArray = false;
            IsStruct = true;
        }

        public void SetNotUsed()
        {
            IsUsed = false;
        }

        internal void SetAutoName(AutoName autoName)
        {
            if (autoName.GetPrecedence() > AutoName.GetPrecedence())
                AutoName = autoName;
        }

        public void HintType(ref TypeContainer container)
        {
            DataType.HintType(ref container);
        }

        public ref TypeContainer GetTypeContainer()
        {
            return ref DataType;
        }
    }
}
