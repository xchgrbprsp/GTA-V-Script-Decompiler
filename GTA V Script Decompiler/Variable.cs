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
        Stack.DataType Type { get; set; }
        public int Index { get; private set; }
        public long Value { get; set; }
        public int ImmediateSize { get; set; }
        public string Name { get; set; } = "";
        public bool Is_Used { get { return IsUsed; } }
        public bool Is_Called { get { return IsCalled; } }
        public bool Is_Array { get { return IsArray; } }
        public Stack.DataType DataType { get { return Type; } set { Type = value; } }

        internal AutoName AutoName { get; private set; } = new DefaultAutoName(Types.GetTypeInfo(Stack.DataType.Unk));

        public Variable(int index)
        {
            Index = index;
            Value = 0;
            ImmediateSize = 1;
            IsArray = false;
            IsUsed = true;
            Type = Stack.DataType.Unk;
        }

        public Variable(int index, long value)
        {
            Index = index;
            Value = value;
            ImmediateSize = 1;
            IsArray = false;
            IsUsed = true;
            Type = Stack.DataType.Unk;
            IsStruct = false;
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
            DataType = Stack.DataType.Unk;
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

        public void HintType(Stack.DataType type)
        {
            if (Types.GetTypeInfo(type) > Types.GetTypeInfo(DataType))
            {
                DataType = type;

                if (AutoName is DefaultAutoName)
                {
                    AutoName = new DefaultAutoName(Types.GetTypeInfo(type));
                }

                if (type == Stack.DataType.Vector3)
                {
                    ImmediateSize = 3;
                }
            }
        }
    }
}
