using System.Collections.Generic;

namespace Decompiler
{
    public class Comment
    {
        public string text;
        private readonly int hashCode;

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
