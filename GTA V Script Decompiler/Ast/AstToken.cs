using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    internal class AstToken
    {
        public readonly Function function;
        public AstToken(Function func)
        {
            function = func;
        }

        public virtual bool IsStatement()
        {
            return false;
        }

        public virtual bool HasSideEffects()
        {
            return false;
        }

        public virtual int GetStackCount()
        {
            return 1;
        }

        public new virtual Stack.DataType GetType()
        {
            // TODO move this somewhere else

            if (CanGetGlobalIndex())
                return Program.globalTypeMgr.GetGlobalType(GetGlobalIndex());

            return Stack.DataType.Unk;
        }

        public virtual void HintType(Stack.DataType type)
        {
            if (CanGetGlobalIndex())
                Program.globalTypeMgr.HintGlobalType(GetGlobalIndex(), type);
        }

        public override string ToString()
        {
            return "StackVal";
        }

        public virtual string ToLiteralString()
        {
            return ToString();
        }

        public virtual string ToPointerString()
        {
            return ToString();
        }
        
        public virtual bool CanGetGlobalIndex()
        {
            return false;
        }

        public virtual int GetGlobalIndex()
        {
            return 0;
        }
    }
}
