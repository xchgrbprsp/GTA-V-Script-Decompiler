using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class NativeCall : FunctionCallBase
    {
        public readonly string Name;
        public readonly NativeDBEntry? Entry;
        public readonly int ReturnCount;

        public NativeCall(Function func, List<AstToken> arguments, string name, ulong hash, int returnCount) : base(func, arguments)
        {
            Name = name;
            Entry = Program.nativeDB.GetEntry(hash);
            ReturnCount = returnCount;

            int i = 0;

            // TODO move this somewhere else!!!!
            foreach (var arg in arguments)
            {
                if (arg.GetStackCount() == 1)
                {
                    arg.HintType(Entry?.GetParamType(i) ?? Stack.DataType.Unk);
                    if (arg is LocalLoad && (Entry?.GetParam(i)?.autoname ?? false))
                        function.SetFrameVarAutoName((arg as LocalLoad).Index, new NativeParameterAutoName(Entry.Value.GetParam(i).name));
                    else if (arg is Local && (Entry?.GetParam(i)?.autoname ?? false))
                        function.SetFrameVarAutoName((arg as Local).Index, new NativeParameterAutoName(Entry.Value.GetParam(i).name));
                }
                else if (arg.GetStackCount() == 3)
                {
                    if (arg is LoadN && (arg as LoadN).Pointer is Local && (Entry?.GetParam(i)?.autoname ?? false))
                    {
                        // function.SetFrameVarAutoName(((arg as LoadN).Pointer as Local).Index, new NativeParameterAutoName("coords"));
                        function.GetFrameVar(((arg as LoadN).Pointer as Local).Index).HintType(Stack.DataType.Vector3);
                    }
                    arg.HintType(Stack.DataType.Vector3);
                }

                i += arg.GetStackCount();
            }
        }

        public override Stack.DataType GetType()
        {
            return Entry?.GetReturnType() ?? base.GetType();
        }

        public override string GetName()
        {
            return Name;
        }

        public override int GetReturnCount()
        {
            return ReturnCount;
        }
    }
}
