using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Decompiler
{
    /// <summary>
    /// Generates a dictionary of indexes and the strings at those given indexed for use with the PushString instruction
    /// </summary>
    public class StringTable
    {
        private readonly byte[] _table;
        private static readonly byte[] _nl = { 92, 110 }, _cr = { 92, 114 }, _qt = { 92, 34 };
        private readonly Dictionary<int, string> _dictionary;
        public StringTable(Stream scriptFile, int[] stringtablelocs, int blockcount, int wholesize)
        {
            _table = new byte[wholesize];
            for (int i = 0, off = 0; i < blockcount; i++, off += 0x4000)
            {
                var tablesize = ((i + 1)*0x4000 >= wholesize) ? wholesize%0x4000 : 0x4000;
                scriptFile.Position = stringtablelocs[i];
                scriptFile.Read(_table, off, tablesize);
            }

            _dictionary = new Dictionary<int, string>();
            List<byte> Working = new(100);
            for (int i = 0, index = 0, max = _table.Length; i<max; i++)
            {
                for (index = i; i < max; i++)
                {
                    var b = _table[i];
                    switch (b)
                    {
                        case 0:
                            goto addString;
                        case 10:
                            Working.AddRange(_nl);
                            break;
                        case 13:
                            Working.AddRange(_cr);
                            break;
                        case 34:
                            Working.AddRange(_qt);
                            break;
                        default:
                            Working.Add(b);
                            break;
                    }
                }

addString:
                _dictionary.Add(index, Encoding.ASCII.GetString(Working.ToArray()).Replace("\\", "\\\\").Replace("\"", "\\\""));
                Working.Clear();
            }
        }

        public bool StringExists(int index)
        {
            return index >= 0 && index < _table.Length;
        }

        public string this[int index]
        {
            get
            {
                if (_dictionary.ContainsKey(index))
                {
                    return _dictionary[index];//keep the fast dictionary access
                }
                //enable support when the string index doesnt fall straight after a null terminator
                if (index < 0 || index >= _table.Length)
                {
                    throw new IndexOutOfRangeException("The index given was outside the range of the String table");
                }

                List<byte> Working = new(100);
                for (int i = index, max = _table.Length; i < max; i++)
                {
                    var b = _table[i];
                    switch (b)
                    {
                        case 0:
                            goto addString;
                        case 10:
                            Working.AddRange(_nl);
                            break;
                        case 13:
                            Working.AddRange(_cr);
                            break;
                        case 34:
                            Working.AddRange(_qt);
                            break;
                        default:
                            Working.Add(b);
                            break;
                    }
                }

addString:
                return Encoding.ASCII.GetString(Working.ToArray());
            }
        }

        public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public int[] Keys
        {
            get { return _dictionary.Keys.ToArray(); }
        }
        public string[] Values
        {
            get { return _dictionary.Values.ToArray(); }
        }
    }
}

