namespace Decompiler.Ast
{
    internal class ConstantInt : AstToken
    {
        private readonly ulong Value;
        private TypeContainer Type = new(Types.INT);

        public ConstantInt(Function func, ulong integer) : base(func) => Value = integer;

        public ulong GetValue() => Value;

        public override ref TypeContainer GetTypeContainer() => ref Type;

        public override string ToString()
        {
            if (Type.Type == Types.BOOL)
            {
                if (Value == 0)
                    return "false";
                else if (Value == 1)
                    return "true";
            }
            else if (Type.Type == Types.FUNCTION && Properties.Settings.Default.ShowFunctionPointers)
            {
                var func = function.ScriptFile.Functions.Find(f => f.Location == (int)Value);

                if (func != null)
                    return "&" + func!.Name;
            }

            var info = Type.Type;
            if (Program.EnumDisplayTypeCache != 0 && info.Enum != null)
            {
                if (info.Enum.TryGetValue((int)Value, out var enumVal))
                {
                    return Program.EnumDisplayTypeCache == 1 ? enumVal : $"{Program.Hashes.GetHash((uint)Value)} /*{enumVal}*/";
                }
            }

            return Program.Hashes.GetHash((uint)Value);
        }
    }
}
