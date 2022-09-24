using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class ConstantInt : AstToken
    {
        readonly UInt64 Value;
        Stack.DataType Type = Stack.DataType.Int;

        public ConstantInt(Function func, UInt64 integer) : base(func)
        {
            Value = integer;
        }

        public override Stack.DataType GetType()
        {
            return Type;
        }

        public UInt64 GetValue()
        {
            return Value;
        }

        public override void HintType(Stack.DataType type)
        {
            Type = Types.GetPrecise(Type, type);
        }

        public override string ToString()
        {
            if (Type == Stack.DataType.Bool)
            {
                if (Value == 0)
                    return "false";
                else if (Value == 1)
                    return "true";
            }

            var info = Types.GetTypeInfo(GetType());
            if (info.Enum != null)
            {
                if (info.Enum!.HasValue((int)Value))
                    return info.Enum!.GetValue((int)Value);
            }    

            return ScriptFile.HashBank.GetHash((int)Value); // todo int style processing
        }
    }
}
