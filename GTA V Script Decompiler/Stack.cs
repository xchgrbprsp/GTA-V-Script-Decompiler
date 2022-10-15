using Decompiler.Ast;
using System;
using System.Collections.Generic;

namespace Decompiler
{
    public class Stack
    {
        readonly List<AstToken> _stack;
        readonly Function Function;

        public Stack(Function func)
        {
            _stack = new List<AstToken>();
            Function = func;
        }

        public void Dispose()
        {
            _stack.Clear();
        }

        internal void Push(AstToken token)
        {
            _stack.Add(token);
        }

        internal AstToken Pop(bool allowMulti = false)
        {
            var index = _stack.Count - 1;
            if (index < 0)
            {
                return new AstToken(Function);
            }

            var val = _stack[index];
            _stack.RemoveAt(index);

            return val.GetStackCount() != 1 && !allowMulti ? throw new InvalidOperationException() : val;
        }

        internal AstToken Peek()
        {
            return _stack.Count == 0 ? new AstToken(Function) : _stack[^1];
        }

        internal AstToken PeekIdx(int index)
        {
            return _stack[^(index + 1)];
        }

        internal AstToken PopVector()
        {
            if (Peek().GetStackCount() == 3)
                return Pop(true);
            else if (_stack.Count >= 3 && PeekIdx(0).GetStackCount() == 1 && PeekIdx(1).GetStackCount() == 1 && PeekIdx(2).GetStackCount() == 1)
            {
                return new Vector(Function, Pop(), Pop(), Pop());
            }

            return new Vector(Function, new(Function), new(Function), new(Function));
        }

        internal List<AstToken> PopCount(int count)
        {
            if (count == 0)
                return new List<AstToken>();

            List<AstToken> values = new();
            var popped = 0;
            while (true)
            {
                if (_stack.Count != 0)
                {
                    var val = Pop(true);

                    if (popped + val.GetStackCount() > count)
                    {
                        //
                    }

                    values.Add(val);
                    popped += val.GetStackCount();
                }
                else
                {
                    values.Add(Pop());
                    popped++;
                }

                if (popped >= count)
                    break;
            }

            values.Reverse();
            return values;
        }

        internal int GetCount()
        {
            var count = 0;

            foreach (var val in _stack)
                count += val.GetStackCount();

            return count;
        }

        #region Opcodes

        /// <summary>
        /// returns a string saying the size of an array if its > 1
        /// </summary>
        /// <param name="immediate"></param>
        /// <returns></returns>
        /// <remarks>TODO: Move this somewhere else</remarks>
        public static string GetArraySizeCmt(uint immediate)
        {
            return !Properties.Settings.Default.ShowArraySize ? "" : immediate == 1 ? "" : " /*" + immediate.ToString() + "*/";
        }

        #endregion
    }
}
