using System;
using System.Linq;

namespace Decompiler
{
    internal abstract class AutoName
    {
        internal enum NameCollisionBehavior
        {
            AddNumberSuffix,
            IncrementCharacter // for loop index
        }

        public abstract string GetName();
        public abstract int GetPrecedence();
        public abstract NameCollisionBehavior GetNameCollisionBehavior();
    }

    internal class DefaultAutoName : AutoName
    {
        private readonly Variable Variable;

        public DefaultAutoName(Variable variable) => Variable = variable;

        public override string GetName() => Variable.DataType.Type.AutoName;

        public override NameCollisionBehavior GetNameCollisionBehavior() => NameCollisionBehavior.AddNumberSuffix;

        public override int GetPrecedence() => 0;
    }

    internal class NativeParameterAutoName : AutoName
    {
        private readonly string Parameter;

        public NativeParameterAutoName(string parameter) => Parameter = parameter;

        public override string GetName() => Parameter;

        public override NameCollisionBehavior GetNameCollisionBehavior() => NameCollisionBehavior.AddNumberSuffix;

        public override int GetPrecedence() => 1;
    }

    internal class NativeReturnAutoName : AutoName
    {
        public static bool CanApply(NativeDBEntry native)
        {
            var name = native.name;

            return name.Contains("GET_") || name.StartsWith("IS") || name.StartsWith("HAS");
        }

        public static string CamelCase(string[] words)
        {
            words[0] = words[0].ToLower();

            for (var i = 1; i < words.Length; i++)
            {
                words[i] = words[i].ToLower();
                words[i] = string.Concat(words[i][0].ToString().ToUpper(), words[i].AsSpan(1));
            }

            return string.Join("", words);
        }

        public static string GetNativeReturnName(string nativeStr)
        {
            var split = nativeStr.Split("_");
            if (split[0] is "IS" or "HAS")
            {
                return CamelCase(split);
            }
            else if (split.Contains("GET"))
            {
                split = nativeStr.Split("GET_");

                return split.Length == 1 ? CamelCase(split[0].Split("_")) : CamelCase(split[1].Split("_"));
            }

            throw new InvalidOperationException("Could not extract name from native");
        }

        private readonly string Name;

        public NativeReturnAutoName(NativeDBEntry native) => Name = GetNativeReturnName(native.name);

        public override string GetName() => Name;

        public override NameCollisionBehavior GetNameCollisionBehavior() => NameCollisionBehavior.AddNumberSuffix;

        public override int GetPrecedence() => 2;
    }

    internal class LoopIndexAutoName : AutoName
    {
        public override string GetName() => "i";

        public override NameCollisionBehavior GetNameCollisionBehavior() => NameCollisionBehavior.IncrementCharacter;

        public override int GetPrecedence() => 3;
    }
}
