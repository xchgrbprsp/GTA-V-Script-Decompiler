using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class String : AstToken
    {
        public readonly AstToken Index;

        public String(Function func, AstToken index) : base(func)
        {
            Index = index;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.StringPtr;
        }

        public override string ToString()
        {
            if (Index is ConstantInt)
            {
                if (Properties.Settings.Default.ShowLocalizedTexts)
                {
                    uint hash = Utils.Joaat(function.Scriptfile.StringTable[(int)(Index as ConstantInt).GetValue()]);

                    if (hash != 0x3acbce85 /*STRING*/ && Program.textDB.Strings.TryGetValue(hash, out string text))
                    {
                        return $"_(\"{text.Replace("\"", "\\\"")}\")";
                    }    
                }

                return "\"" + function.Scriptfile.StringTable[(int)(Index as ConstantInt).GetValue()] + "\"";
            }
            else
            {
                return "StringTable(" + Index.ToString() + ")";
            }
        }
    }
}
