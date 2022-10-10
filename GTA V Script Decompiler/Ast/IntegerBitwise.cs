using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class IntegerAnd : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerAnd(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Rhs.GetTypeContainer());
        }

        bool IsLogicalOperation()
        {
            return Lhs is not ConstantInt && Rhs is not ConstantInt;
        }

        public override ref TypeContainer GetTypeContainer()
        {
            var type = IsLogicalOperation() ? ref Types.BOOL : ref Types.INT;
            return ref type.GetContainer();
        }

        public override string ToString()
        {
            if (IsLogicalOperation())
                return Lhs.ToString() + " && " + Rhs.ToString();
            else
                return Lhs.ToString() + " & " + Rhs.ToString();
        }
    }

    internal class IntegerOr : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerOr(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
            Lhs.HintType(ref Rhs.GetTypeContainer());
        }

        bool IsLogicalOperation()
        {
            return Lhs is not ConstantInt && Rhs is not ConstantInt;
        }

        public override ref TypeContainer GetTypeContainer()
        {
            var type = IsLogicalOperation() ? ref Types.BOOL : ref Types.INT;
            return ref type.GetContainer();
        }

        public override string ToString()
        {
            if (IsLogicalOperation())
                return Lhs.ToString() + " || " + Rhs.ToString();
            else
                return Lhs.ToString() + " | " + Rhs.ToString();
        }
    }

    internal class IntegerXor : AstToken
    {
        AstToken Lhs;
        AstToken Rhs;
        public IntegerXor(Function func, AstToken rhs, AstToken lhs) : base(func)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public override ref TypeContainer GetTypeContainer()
        {
            return ref Types.INT.GetContainer();
        }

        public override string ToString()
        {
            return Lhs.ToString() + " ^ " + Rhs.ToString();
        }
    }
}
