using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class StoreN : AstToken
    {
        readonly AstToken Pointer;
        readonly AstToken Count;
        readonly List<AstToken> Values;

        public StoreN(Function func, AstToken pointer, AstToken count, List<AstToken> values) : base(func)
        {
            Pointer = pointer;
            Count = count;
            Values = values;

            if (pointer is Local && values.Count == 1 && values[0] is NativeCall && count is ConstantInt && (count as ConstantInt).GetValue() == 3)
            {
                pointer.HintType(Stack.DataType.Vector3Ptr); // almost always true

                var entry = (values[0] as NativeCall).Entry;
                if (entry != null)
                {
                    if (NativeReturnAutoName.CanApply(entry.Value))
                        function.SetFrameVarAutoName((pointer as Local).Index, new NativeReturnAutoName(entry.Value));
                }
            }

            if (values.Count == 1 && values[0].GetType() != Stack.DataType.Unk)
            {
                if (Types.HasPointerVersion(values[0].GetType()))
                    pointer.HintType(Types.GetPointerVersion(values[0].GetType()));
            }
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            string res = (Pointer.IsPointer() ? Pointer.ToPointerString() : ("*" + Pointer.ToString())) + " = { ";
            foreach (var value in Values)
                res += value + ", ";
            return res.Remove(res.Length - 2) + " };";
        }
    }
}
