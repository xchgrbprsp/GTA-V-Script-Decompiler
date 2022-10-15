using System.Collections.Generic;

namespace Decompiler.Ast
{
    internal class TextLabelAssignString : AstToken
    {
        public readonly AstToken Dst;
        public readonly AstToken Src;
        public readonly int Size;
        public TextLabelAssignString(Function func, AstToken dst, AstToken src, int size) : base(func)
        {
            Dst = dst;
            Src = src;
            Size = size;

            Dst.HintType(ref Types.PSTRING.GetContainer());
            Src.HintType(ref Types.PSTRING.GetContainer());
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            return "TEXT_LABEL_ASSIGN_STRING(" + Dst.ToString() + ", " + Src.ToString() + ", " + Size + ");";
        }
    }

    internal class TextLabelAppendString : AstToken
    {
        public readonly AstToken Dst;
        public readonly AstToken Src;
        public readonly int Size;
        public TextLabelAppendString(Function func, AstToken dst, AstToken src, int size) : base(func)
        {
            Dst = dst;
            Src = src;
            Size = size;

            Dst.HintType(ref Types.PSTRING.GetContainer());
            Src.HintType(ref Types.PSTRING.GetContainer());
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            return "TEXT_LABEL_APPEND_STRING(" + Dst.ToString() + ", " + Src.ToString() + ", " + Size + ");";
        }
    }

    internal class TextLabelAssignInt : AstToken
    {
        public readonly AstToken Dst;
        public readonly AstToken Integer;
        public readonly int Size;
        public TextLabelAssignInt(Function func, AstToken dst, AstToken integer, int size) : base(func)
        {
            Dst = dst;
            Integer = integer;
            Size = size;

            Integer.HintType(ref Types.INT.GetContainer());
            Dst.HintType(ref Types.PSTRING.GetContainer());
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            return "TEXT_LABEL_ASSIGN_INT(" + Dst.ToString() + ", " + Integer.ToString() + ", " + Size + ");";
        }
    }

    internal class TextLabelAppendInt : AstToken
    {
        public readonly AstToken Dst;
        public readonly AstToken Integer;
        public readonly int Size;
        public TextLabelAppendInt(Function func, AstToken dst, AstToken integer, int size) : base(func)
        {
            Dst = dst;
            Integer = integer;
            Size = size;

            Integer.HintType(ref Types.INT.GetContainer());
            Dst.HintType(ref Types.PSTRING.GetContainer());
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            return "TEXT_LABEL_APPEND_INT(" + Dst.ToString() + ", " + Integer.ToString() + ", " + Size + ");";
        }
    }

    internal class TextLabelCopy : AstToken
    {
        public readonly AstToken Dst;
        public readonly List<AstToken> Values;
        public readonly AstToken Value;

        public TextLabelCopy(Function func, AstToken dst, List<AstToken> values, AstToken value) : base(func)
        {
            Dst = dst;
            Values = values;
            Value = value;
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            var res = "{ ";
            foreach (var value in Values)
                res += value + ", ";
            res = res.Remove(res.Length - 2) + " }";
            return "TEXT_LABEL_COPY(" + Dst.ToString() + ", " + res + ", " + Value.ToString() + ");";
        }
    }
}
