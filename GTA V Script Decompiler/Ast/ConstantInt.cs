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
        readonly ulong Value;
        Stack.DataType Type = Stack.DataType.Int;

        public ConstantInt(Function func, ulong integer) : base(func)
        {
            Value = integer;
        }

        public override Stack.DataType GetType()
        {
            return Type;
        }

        public ulong GetValue()
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
            else if (Type == Stack.DataType.Function && Properties.Settings.Default.ShowFunctionPointers)
            {
                var func = function.ScriptFile.Functions.Find(f => f.Location == (int)Value);

                if (func != null)
                    return "&" + func!.Name;
            }

            var info = Types.GetTypeInfo(GetType());
            if (info.Enum != null)
            {
                if (info.Enum.TryGetValue((int)Value, out string enumVal))
                    return enumVal;
            }    

            return ScriptFile.HashBank.GetHash((uint)Value); // todo int style processing
        }
    }
}
