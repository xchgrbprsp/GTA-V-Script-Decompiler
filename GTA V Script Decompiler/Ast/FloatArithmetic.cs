using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class FloatAdd : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatAdd(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.FLOAT.GetContainer());
            Rhs.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.FLOAT.GetContainer();
        }

        public override string ToString()
        {
            return Lhs.ToString() + " + " + Rhs.ToString();
        }
    }

    internal class FloatDiv : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatDiv(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.FLOAT.GetContainer());
            Rhs.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.FLOAT.GetContainer();
        }

        public override string ToString()
        {
            return Lhs.ToString() + " / " + Rhs.ToString();
        }
    }

    internal class FloatMod : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatMod(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.FLOAT.GetContainer());
            Rhs.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.FLOAT.GetContainer();
        }

        public override string ToString()
        {
            return Lhs.ToString() + " % " + Rhs.ToString();
        }
    }

    internal class FloatMul : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;

        public FloatMul(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.FLOAT.GetContainer());
            Rhs.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.FLOAT.GetContainer();
        }

        public override string ToString()
        {
            return Lhs.ToString() + " * " + Rhs.ToString();
        }
    }

    internal class FloatNeg : AstToken
    {
        AstToken value;
        public FloatNeg(Function func, AstToken value) : base(func)
        {
            this.value = value;
            this.value.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.FLOAT.GetContainer();
        }

        public override string ToString()
        {
            return "-" + value.ToString();
        }
    }

    internal class FloatSub : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatSub(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.FLOAT.GetContainer());
            Rhs.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.FLOAT.GetContainer();
        }

        public override string ToString()
        {
            return Lhs.ToString() + " - " + Rhs.ToString();
        }
    }
}
