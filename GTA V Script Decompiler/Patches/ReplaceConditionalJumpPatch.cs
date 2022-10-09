using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Patches
{
    internal class ReplaceConditionalJumpPatch : Patch
    {
        public ReplaceConditionalJumpPatch(Function function) : base(function)
        {
        }

        public override string GetName(int start, int end)
        {
            return $"Replace Conditional Jump With DROP";
        }

        public override byte[] GetPatch(int start, int end)
        {
            List<byte> bytes = new();

            for (int i = start; i < end; i++)
            {
                bytes.Add((byte)Opcode.DROP);

                foreach (var _ in Function.Instructions[i].Operands)
                {
                    bytes.Add(0);
                }
            }

            return bytes.ToArray();
        }

        public override bool ShouldEnablePatch(int start, int end)
        {
            return true;
        }

        public override bool ShouldShowPatch(int start, int end)
        {
            return Function.Instructions[start].IsConditionJump;
        }
    }
}
