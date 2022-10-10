using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class VectorAdd : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public VectorAdd(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.VEC3.GetContainer();
        }

        public override int GetStackCount()
        {
            return 3;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " + " + Rhs.ToString();
        }
    }

    internal class VectorDiv : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public VectorDiv(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.VEC3.GetContainer();
        }

        public override int GetStackCount()
        {
            return 3;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " / " + Rhs.ToString();
        }
    }

    internal class VectorMul : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public VectorMul(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.VEC3.GetContainer();
        }

        public override int GetStackCount()
        {
            return 3;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " * " + Rhs.ToString();
        }
    }

    internal class VectorNeg : AstToken
    {
        AstToken value;
        public VectorNeg(Function func, AstToken value) : base(func)
        {
            this.value = value;
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.VEC3.GetContainer();
        }

        public override int GetStackCount()
        {
            return 3;
        }

        public override string ToString()
        {
            return "-" + value.ToString();
        }
    }

    internal class VectorSub : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public VectorSub(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.VEC3.GetContainer();
        }

        public override int GetStackCount()
        {
            return 3;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " - " + Rhs.ToString();
        }
    }
}
