using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class Array : AstToken
    {
        public readonly uint Size;
        public readonly AstToken Pointer;
        public readonly AstToken Index;

        public Array(Function func, uint size, AstToken pointer, AstToken index) : base(func)
        {
            Size = size;
            Pointer = pointer;
            Index = index;
        }

        public override string ToString()
        {
            return "&" + Pointer.ToString() + "[" + Index.ToString() + Stack.GetArraySizeCmt(Size) + "]";
        }

        public override string ToPointerString()
        {
            return Pointer.ToPointerString() + "[" + Index.ToString() + Stack.GetArraySizeCmt(Size) + "]";
        }
    }

    internal class ArrayLoad : AstToken
    {
        public readonly uint Size;
        public readonly AstToken Pointer;
        public readonly AstToken Index;

        public ArrayLoad(Function func, uint size, AstToken pointer, AstToken index) : base(func)
        {
            Size = size;
            Pointer = pointer;
            Index = index;
        }

        public override string ToString()
        {
            return Pointer.ToPointerString() + "[" + Index.ToString() + Stack.GetArraySizeCmt(Size) + "]";
        }
    }

    internal class ArrayStore : AstToken
    {
        public readonly uint Size;
        public readonly AstToken Pointer;
        public readonly AstToken Index;
        public readonly AstToken Value;

        public ArrayStore(Function func, uint size, AstToken pointer, AstToken index, AstToken value) : base(func)
        {
            Size = size;
            Pointer = pointer;
            Index = index;
            Value = value;
        }

        public override bool IsStatement()
        {
            return true;
        }

        public override string ToString()
        {
            return Pointer.ToPointerString() + "[" + Index.ToString() + Stack.GetArraySizeCmt(Size) + "] = " + Value.ToString() + ";";
        }
    }
}
