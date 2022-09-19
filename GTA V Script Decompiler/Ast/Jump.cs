using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Ast
{
    /// <summary>
    /// Placeholder for unhandled jumps
    /// </summary>
    internal class Jump : AstToken
    {
        public readonly int Offset;
        public Jump(Function func, int offset) : base(func)
        {
            Offset = offset;
        }

        public override bool IsStatement()
        {
            return true;
        }

        /// <summary>
        /// Should ideally not be called
        /// </summary>
        public override string ToString()
        {
            return "goto 0x" + Offset.ToString("X") + ";";
        }
    }
}
