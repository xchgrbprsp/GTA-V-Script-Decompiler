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

        public NativeCall(Function func, List<AstToken> arguments, string name, UInt64 hash, int returnCount) : base(func, arguments)
        {
            Name = name;
            Entry = Program.nativeDB.GetEntry(hash);
            ReturnCount = returnCount;
            int i = 0;
            foreach (var arg in arguments)
            {
                if (arg.GetStackCount() == 1)
                    arg.HintType(Entry?.GetParamType(i) ?? Stack.DataType.Unk);
                i += arg.GetStackCount();
            }
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
