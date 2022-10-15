using System;

namespace Decompiler.Ast
{
    internal class String : AstToken
    {
        public readonly AstToken Index = null;
        public readonly string _string = null;

        public String(Function func, AstToken index) : base(func)
        {
            Index = index;
            Index.HintType(ref Types.INT.GetContainer());
        }

        public String(Function func, string @string) : base(func) => _string = @string;

        public override ref TypeContainer GetTypeContainer() => ref Types.PSTRING.GetContainer();

        public string GetString()
        {
            return _string ?? (Index is ConstantInt
                ? function.ScriptFile.StringTable[(int)(Index as ConstantInt).GetValue()]
                : throw new InvalidOperationException("Index is not constant"));
        }

        public override string ToString()
        {
            if (_string != null)
                return "\"" + _string + "\"";

            if (Index is ConstantInt)
            {
                if (Properties.Settings.Default.LocalizedTextType != 0)
                {
                    var hash = Utils.Joaat(function.ScriptFile.StringTable[(int)(Index as ConstantInt).GetValue()]);

                    if (hash != 0x3acbce85 /*STRING*/ && Program.textDB.Strings.TryGetValue(hash, out var text))
                    {
                        return Properties.Settings.Default.LocalizedTextType == 1
                            ? $"_(\"{text.Replace("\"", "\\\"")}\")"
                            : $"\"{function.ScriptFile.StringTable[(int)(Index as ConstantInt).GetValue()]}\" /*{text}*/";
                    }
                }

                return "\"" + function.ScriptFile.StringTable[(int)(Index as ConstantInt).GetValue()] + "\"";
            }
            else
            {
                return "StringTable(" + Index.ToString() + ")";
            }
        }
    }
}
