using System.Collections.Generic;

namespace Decompiler.Patches
{
    internal class NopInstructionPatch : Patch
    {
        public NopInstructionPatch(Function function) : base(function)
        {
        }

        public override string GetName(int start, int end)
        {
            return $"Nop Instruction{(end - start != 1 ? "s" : "")}";
        }

        public override byte[] GetPatch(int start, int end)
        {
            List<byte> bytes = new();

            for (int i = start; i < end; i++)
            {
                bytes.Add(0);

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
            return true;
        }
    }
}
