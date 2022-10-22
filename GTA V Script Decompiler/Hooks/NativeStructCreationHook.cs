using Decompiler.Ast;
using System.Collections.Generic;

namespace Decompiler.Hooks
{
    internal class GetEventDataStructCreationHook : NativeHook
    {
        public override string Native => "GET_EVENT_DATA";

        public override bool Hook(Function func, List<AstToken> args, Stack stack)
        {
            if (args[2] is Local && args[3] is ConstantInt)
            {
                func.GetFrameVar((args[2] as Local).Index).SetStruct((int)(args[3] as ConstantInt).GetValue());
            }

            return false;
        }
    }

    internal class SendTUScriptEventStructCreationHook : NativeHook
    {
        public override string Native => "SEND_TU_SCRIPT_EVENT";

        public override bool Hook(Function func, List<AstToken> args, Stack stack)
        {
            if (args[1] is Local && args[2] is ConstantInt)
            {
                func.GetFrameVar((args[1] as Local).Index).SetStruct((int)(args[2] as ConstantInt).GetValue());
            }

            return false;
        }
    }

    internal class StartNewScriptWithArgsStructCreationHook : NativeHook
    {
        public override string Native => "START_NEW_SCRIPT_WITH_ARGS";

        public override bool Hook(Function func, List<AstToken> args, Stack stack)
        {
            if (args[1] is Local && args[2] is ConstantInt)
            {
                func.GetFrameVar((args[1] as Local).Index).SetStruct((int)(args[2] as ConstantInt).GetValue());
            }

            return false;
        }
    }

    internal class StartNewScriptWithNameHashAndArgsStructCreationHook : StartNewScriptWithArgsStructCreationHook
    {
        public override string Native => "START_NEW_SCRIPT_WITH_NAME_HASH_AND_ARGS";
    }
}
