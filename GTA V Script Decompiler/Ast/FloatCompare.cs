using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class FloatEq : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatEq(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.FLOAT.GetContainer());
            Rhs.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.BOOL.GetContainer();
        }

        public override string ToString()
        {
            return Lhs.ToString() + " == " + Rhs.ToString();
        }
    }

    internal class FloatNe : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatNe(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.FLOAT.GetContainer());
            Rhs.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.BOOL.GetContainer();
        }

        public override string ToString()
        {
            return Lhs.ToString() + " != " + Rhs.ToString();
        }
    }

    internal class FloatLt : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatLt(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.FLOAT.GetContainer());
            Rhs.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.BOOL.GetContainer();
        }

        public override string ToString()
        {
            return Lhs.ToString() + " < " + Rhs.ToString();
        }
    }

    internal class FloatLe : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;

        public FloatLe(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.FLOAT.GetContainer());
            Rhs.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.BOOL.GetContainer();
        }

        public override string ToString()
        {
            return Lhs.ToString() + " <= " + Rhs.ToString();
        }
    }

    internal class FloatGt : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatGt(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.FLOAT.GetContainer());
            Rhs.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.BOOL.GetContainer();
        }

        public override string ToString()
        {
            return Lhs.ToString() + " > " + Rhs.ToString();
        }
    }

    internal class FloatGe : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public FloatGe(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.FLOAT.GetContainer());
            Rhs.HintType(ref Types.FLOAT.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.BOOL.GetContainer();
        }

        public override string ToString()
        {
            return Lhs.ToString() + " > " + Rhs.ToString();
        }
    }
}
