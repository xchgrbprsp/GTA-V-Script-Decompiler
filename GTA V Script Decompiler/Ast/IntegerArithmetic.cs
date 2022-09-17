using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class IntegerAdd : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerAdd(Function func, AstToken lhs, AstToken rhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Int;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " + " + Rhs.ToString();
        }
    }

    internal class IntegerDiv : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerDiv(Function func, AstToken lhs, AstToken rhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Int;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " / " + Rhs.ToString();
        }
    }

    internal class IntegerMod : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerMod(Function func, AstToken lhs, AstToken rhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Int;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " % " + Rhs.ToString();
        }
    }

    internal class IntegerMul : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerMul(Function func, AstToken lhs, AstToken rhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Int;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " * " + Rhs.ToString();
        }
    }

    internal class IntegerNeg : AstToken
    {
        AstToken value;
        public IntegerNeg(Function func, AstToken value) : base(func)
        {
            this.value = value;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Int;
        }

        public override string ToString()
        {
            return "-" + value.ToString();
        }
    }

    internal class IntegerNot : AstToken
    {
        AstToken value;
        public IntegerNot(Function func, AstToken value) : base(func)
        {
            this.value = value;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Bool;
        }

        public override string ToString()
        {
            return "!" + value.ToString();
        }
    }

    internal class IntegerSub : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerSub(Function func, AstToken lhs, AstToken rhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override Stack.DataType GetType()
        {
            return Stack.DataType.Int;
        }

        public override string ToString()
        {
            return Lhs.ToString() + " - " + Rhs.ToString();
        }
    }
}
