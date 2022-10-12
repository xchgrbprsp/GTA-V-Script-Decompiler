using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public String(Function func, string @string) : base(func)
        {
            _string = @string;
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.PSTRING.GetContainer();
        }

        public string GetString()
        {
            if (_string != null)
                return _string;

            if (Index is ConstantInt)
            {
                return function.ScriptFile.StringTable[(int)(Index as ConstantInt).GetValue()];
            }
            else
            {
                throw new InvalidOperationException("Index is not constant");
            }
        }

        public override string ToString()
        {
            if (_string != null)
                return "\"" + _string + "\"";

            if (Index is ConstantInt)
            {
                if (Properties.Settings.Default.LocalizedTextType != 0)
                {
                    uint hash = Utils.Joaat(function.ScriptFile.StringTable[(int)(Index as ConstantInt).GetValue()]);

                    if (hash != 0x3acbce85 /*STRING*/ && Program.textDB.Strings.TryGetValue(hash, out string text))
                    {
                        if (Properties.Settings.Default.LocalizedTextType == 1)
                            return $"_(\"{text.Replace("\"", "\\\"")}\")";
                        else
                            return $"\"{function.ScriptFile.StringTable[(int)(Index as ConstantInt).GetValue()]}\" /*{text.Replace("\"", "\\\"")}*/";
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
