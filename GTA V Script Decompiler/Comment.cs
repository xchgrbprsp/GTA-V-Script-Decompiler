using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler
{
    public class Comment
    {
        public string text;
        private int hashCode;

        public Comment(string str)
        {
            text = str;
            hashCode = text.GetHashCode();
        }

        public override int GetHashCode()
        {
            return hashCode;
        }

        public override string ToString()
        {
            return text;
        }
    }

    public class Comments : List<Comment>
    {
        public Comments() { }

        public void AddComment(Comment cmt)
        {
            foreach (var c in this)
            {
                if (c == cmt)
                    return;
            }

            Add(cmt);
        }
    }
}
