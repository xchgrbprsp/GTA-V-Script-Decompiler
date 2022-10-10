using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler
{
    internal abstract class AutoName
    {
        internal enum NameCollisionBehavior
        {
            AddNumberSuffix,
            IncrementCharacter // for loop index
        }

        abstract public string GetName();
        abstract public int GetPrecedence();
        abstract public NameCollisionBehavior GetNameCollisionBehavior();
    }

    internal class DefaultAutoName : AutoName
    {
        readonly Variable Variable;

        public DefaultAutoName(Variable variable)
        {
            Variable = variable;
        }

        public override string GetName()
        {
            return Variable.DataType.Type.AutoName;
        }

        public override NameCollisionBehavior GetNameCollisionBehavior()
        {
            return NameCollisionBehavior.AddNumberSuffix;
        }

        public override int GetPrecedence()
        {
            return 0;
        }
    }

    internal class NativeParameterAutoName : AutoName
    {
        readonly string Parameter;

        public NativeParameterAutoName(string parameter)
        {
            Parameter = parameter;
        }

        public override string GetName()
        {
            return Parameter;
        }

        public override NameCollisionBehavior GetNameCollisionBehavior()
        {
            return NameCollisionBehavior.AddNumberSuffix;
        }

        public override int GetPrecedence()
        {
            return 1;
        }
    }

    internal class NativeReturnAutoName : AutoName
    {
        public static bool CanApply(NativeDBEntry native)
        {
            string name = native.name;

            return name.Contains("GET_") || name.StartsWith("IS") || name.StartsWith("HAS");
        }

        public static string CamelCase(string[] words)
        {
            words[0] = words[0].ToLower();

            for (int i = 1; i < words.Length; i++)
            {
                words[i] = words[i].ToLower();
                words[i] = string.Concat(words[i][0].ToString().ToUpper(), words[i].AsSpan(1));
            }

            return string.Join("", words);
        }

        public static string GetNativeReturnName(string nativeStr)
        {
            var split = nativeStr.Split("_");
            if (split[0] == "IS" || split[0] == "HAS")
            {
                return CamelCase(split);
            }
            else if (split.Contains("GET"))
            {
                split = nativeStr.Split("GET_");

                if (split.Length == 1)
                    return CamelCase(split[0].Split("_"));
                else
                    return CamelCase(split[1].Split("_"));
            }

            throw new InvalidOperationException("Could not extract name from native");
        }

        readonly string Name;

        public NativeReturnAutoName(NativeDBEntry native)
        {
            Name = GetNativeReturnName(native.name);
        }

        public override string GetName()
        {
            return Name;
        }

        public override NameCollisionBehavior GetNameCollisionBehavior()
        {
            return NameCollisionBehavior.AddNumberSuffix;
        }

        public override int GetPrecedence()
        {
            return 2;
        }
    }

    internal class LoopIndexAutoName : AutoName
    {
        public override string GetName()
        {
            return "i";
        }

        public override NameCollisionBehavior GetNameCollisionBehavior()
        {
            return NameCollisionBehavior.IncrementCharacter;
        }

        public override int GetPrecedence()
        {
            return 3;
        }
    }
}
