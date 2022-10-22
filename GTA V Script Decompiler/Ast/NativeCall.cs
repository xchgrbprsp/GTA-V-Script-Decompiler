using System.Collections.Generic;

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
            Entry = Program.NativeDB.GetEntry(hash);
            ReturnCount = returnCount;

            var i = 0;

            // TODO move this somewhere else!!!!
            foreach (var arg in arguments)
            {
                if (arg.GetStackCount() == 1)
                {
                    var type = Entry?.GetParamType(i) ?? Types.UNKNOWN;
                    arg.HintType(ref type.GetContainer());

                    if (arg is LocalLoad && (Entry?.GetParam(i)?.AutoName ?? false))
                        function.SetFrameVarAutoName((arg as LocalLoad).Index, new NativeParameterAutoName(Entry.GetParam(i).name));
                    else if (arg is Local && (Entry?.GetParam(i)?.AutoName ?? false))
                        function.SetFrameVarAutoName((arg as Local).Index, new NativeParameterAutoName(Entry.GetParam(i).name));
                }
                else if (arg.GetStackCount() == 3)
                {
                    if (arg is LoadN && (arg as LoadN).Pointer is Local && (Entry?.GetParam(i)?.AutoName ?? false))
                    {
                        function.GetFrameVar(((arg as LoadN).Pointer as Local).Index).HintType(ref Types.VEC3.GetContainer());
                    }

                    arg.HintType(ref Types.VEC3.GetContainer());
                }

                i += arg.GetStackCount();
            }
        }

        public override ref TypeContainer GetTypeContainer()
        {
            var type = Entry?.GetReturnType() ?? Types.UNKNOWN;

            return ref type == Types.VOID ? ref Types.UNKNOWN.GetContainer() : ref type.GetContainer();
        }

        public override string GetName() => Name;

        public override int GetReturnCount() => ReturnCount;
    }
}
