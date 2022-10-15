using Decompiler.Ast;

using String = Decompiler.Ast.String;

namespace Decompiler
{
    internal static class StringObfuscation
    {
        public const uint GET_STRING_WITH_ROTATE_HASH = 0x3270A688;
        public const uint GET_STRING_WITH_ROTATE_2_HASH = 0xFA2B0B15;
        public const uint REORDER_STRING_4_64_HASH = 0xD877850F;
        public const uint REORDER_STRING_8_64_HASH = 0x8859FAB6;
        public const uint REORDER_STRING_4_32_HASH = 0x1826CAD6;
        public const uint REORDER_STRING_4_24_HASH = 0x9264C97B;

        public static void GetStringWithRotate(Function func, Ast.AstToken[] args, Stack stack)
        {
            int iParam0 = (int)(args[0] as ConstantInt).GetValue();
            iParam0 = 32 - iParam0;

            switch (iParam0)
            {
                case 0:
                    stack.Push(new String(func, "port"));
                    break;

                case 1:
                    stack.Push(new String(func, "al"));
                    break;

                case 2:
                    stack.Push(new String(func, "ry"));
                    break;

                case 3:
                    stack.Push(new String(func, "n"));
                    break;

                case 4:
                    stack.Push(new String(func, "WAR"));
                    break;

                case 5:
                    stack.Push(new String(func, "bar"));
                    break;

                case 6:
                    stack.Push(new String(func, "m"));
                    break;

                case 7:
                    stack.Push(new String(func, "dset"));
                    break;

                case 8:
                    stack.Push(new String(func, "lie"));
                    break;

                case 9:
                    stack.Push(new String(func, "s"));
                    break;

                case 10:
                    stack.Push(new String(func, "01"));
                    break;

                case 11:
                    stack.Push(new String(func, "n_DEA"));
                    break;

                case 12:
                    stack.Push(new String(func, "w"));
                    break;

                case 13:
                    stack.Push(new String(func, "_1_tele"));
                    break;

                case 14:
                    stack.Push(new String(func, "_aln"));
                    break;

                case 15:
                    stack.Push(new String(func, "_R"));
                    break;

                case 16:
                    stack.Push(new String(func, "_01_soun"));
                    break;

                case 17:
                    stack.Push(new String(func, "IE"));
                    break;

                case 18:
                    stack.Push(new String(func, "pa"));
                    break;

                case 19:
                    stack.Push(new String(func, "t"));
                    break;

                case 20:
                    stack.Push(new String(func, "scr"));
                    break;

                case 21:
                    stack.Push(new String(func, "rc"));
                    break;

                case 22:
                    stack.Push(new String(func, "th"));
                    break;

                case 23:
                    stack.Push(new String(func, "a"));
                    break;

                case 24:
                    stack.Push(new String(func, "p_in"));
                    break;

                case 25:
                    stack.Push(new String(func, "_scene"));
                    break;

                case 26:
                    stack.Push(new String(func, "_GR_"));
                    break;

                case 27:
                    stack.Push(new String(func, "dlc_gr"));
                    break;

                case 28:
                    stack.Push(new String(func, "_CS2_"));
                    break;

                case 29:
                    stack.Push(new String(func, "oneshot"));
                    break;

                case 30:
                    stack.Push(new String(func, "General"));
                    break;

                case 31:
                    stack.Push(new String(func, "sounds"));
                    break;

                case 32:
                default:
                    stack.Push(new String(func, ""));
                    break;
            }
        }

        public static void ReorderString_4(Function func, Ast.AstToken[] args, Stack stack, int size = 8)
        {
            stack.Push(new String(func,
                (args[1] as String).GetString() +
                (args[0] as String).GetString() +
                (args[3] as String).GetString() +
                (args[2] as String).GetString()
            ));

            for (int i = 0; i < (size - 1); i++)
                stack.Push(new String(func, ""));
        }

        public static void ReorderString_8(Function func, Ast.AstToken[] args, Stack stack)
        {
            stack.Push(new String(func,
                (args[4] as String).GetString() +
                (args[1] as String).GetString() +
                (args[7] as String).GetString() +
                (args[5] as String).GetString() +
                (args[6] as String).GetString() +
                (args[2] as String).GetString() +
                (args[0] as String).GetString() +
                (args[3] as String).GetString()
            ));

            for (int i = 0; i < 15; i++)
                stack.Push(new String(func, ""));
        }
    }
}
