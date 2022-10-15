using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal abstract class VectorArithmetic : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        protected abstract char Symbol { get; }

        protected VectorArithmetic(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Types.VEC3.GetContainer());
            Rhs.HintType(ref Types.VEC3.GetContainer());
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.VEC3.GetContainer();
        }

        public override int GetStackCount()
        {
            return 3;
        }

        public bool IsComplexOperand(AstToken operand)
        {
            if (operand is not VectorArithmetic)
                return false;
            else
                return this.GetType() != operand.GetType();
        }

        public override string ToString()
        {
            string lhs = IsComplexOperand(Lhs) ? "(" + Lhs.ToString() + ")" : Lhs.ToString();
            string rhs = IsComplexOperand(Rhs) ? "(" + Rhs.ToString() + ")" : Rhs.ToString();

            return $"{lhs} {Symbol} {rhs}";
        }
    }

    internal class VectorAdd : VectorArithmetic
    {
        public VectorAdd(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '+';
    }

    internal class VectorSub : VectorArithmetic
    {
        public VectorSub(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '-';
    }

    internal class VectorMul : VectorArithmetic
    {
        public VectorMul(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '*';
    }

    internal class VectorDiv : VectorArithmetic
    {
        public VectorDiv(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '/';
    }

    internal class VectorMod : VectorArithmetic
    {
        public VectorMod(Function func, AstToken rhs, AstToken lhs) : base(func, rhs, lhs)
        {
        }

        protected override char Symbol => '%';
    }

    internal class VectorNeg : AstToken
    {
        AstToken value;
        public VectorNeg(Function func, AstToken value) : base(func)
        {
            this.value = value;
            this.value.HintType(ref Types.INT.GetContainer());
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
            if (value is VectorArithmetic)
                return $"-({value})";
            else
                return $"-{value}";
        }
    }
}
