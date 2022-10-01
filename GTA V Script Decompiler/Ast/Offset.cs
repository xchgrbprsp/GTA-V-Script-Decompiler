using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Offset : AstToken
    {
        public readonly AstToken value;
        public readonly AstToken offset;

        public Offset(Function func, AstToken value, AstToken offset) : base(func)
        {
            this.value = value;
            this.offset = offset;
        }

        public override string ToString()
        {
            var sep = "->";
            if (value is Local or Global or Offset or Array)
                sep = ".";

            if (offset is ConstantInt)
                return "&(" + value.ToPointerString() + sep + "f_" + offset.ToString() + ")";
            else
                return "&(" + value.ToPointerString() + sep + "f_[" + offset.ToString() + "])";
        }

        public override string ToPointerString()
        {
            var sep = "->";
            if (value is Local or Global or Offset or Array)
                sep = ".";

            if (offset is ConstantInt)
                return value.ToPointerString() + sep + "f_" + (offset as ConstantInt).GetValue();
            else
                return value.ToPointerString() + sep + "f_[" + offset.ToString() + "]";
        }

        public override bool CanGetGlobalIndex()
        {
            return value.CanGetGlobalIndex() && offset is ConstantInt;
        }

        public override int GetGlobalIndex()
        {
            return value.GetGlobalIndex() + (int)((offset as ConstantInt).GetValue());
        }
    }

    internal class OffsetLoad : AstToken
    {
        public readonly AstToken value;
        public readonly int offset;

        public OffsetLoad(Function func, AstToken value, int offset) : base(func)
        {
            this.value = value;
            this.offset = offset;
        }

        public override string ToString()
        {
            var sep = "->";
            if (value is Static or Global or Offset or Array)
                sep = ".";

            return value.ToPointerString() + sep + "f_" + offset.ToString();
        }

        public override bool CanGetGlobalIndex()
        {
            return value.CanGetGlobalIndex();
        }

        public override int GetGlobalIndex()
        {
            return value.GetGlobalIndex() + offset;
        }
    }

    internal class OffsetStore : AstToken
    {
        public readonly AstToken value;
        public readonly AstToken storedValue;
        public readonly int offset;

        public OffsetStore(Function func, AstToken value, int offset, AstToken storedValue) : base(func)
        {
            this.value = value;
            this.offset = offset;
            this.storedValue = storedValue;

            HintType(storedValue.GetType());
            storedValue.HintType(GetType());
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            var sep = "->";
            if (value is Static or Global or Offset or Array)
                sep = ".";

            return value.ToPointerString() + sep + "f_" + offset.ToString() + " = " + storedValue.ToString() + ";";
        }
    }
}
