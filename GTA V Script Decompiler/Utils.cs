using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Decompiler
{
	static class Utils
	{
		public static uint Joaat(string str)
		{
			uint hash, i;
			char[] key = str.ToLower().ToCharArray();
			for (hash = i = 0; i < key.Length; i++)
			{
				hash += key[i];
				hash += (hash << 10);
				hash ^= (hash >> 6);
			}
			hash += (hash << 3);
			hash ^= (hash >> 11);
			hash += (hash << 15);
			return hash;
		}

		public static string Represent(long value, Stack.DataType type)
		{
			switch (type)
			{
				case Stack.DataType.Float:
					return BitConverter.ToSingle(BitConverter.GetBytes(value), 0).ToString() + "f";
				case Stack.DataType.Bool:
					break;//return value == 0 ? "false" : "true";				//still need to fix bools
				case Stack.DataType.FloatPtr:
				case Stack.DataType.IntPtr:
				case Stack.DataType.StringPtr:
				case Stack.DataType.UnkPtr:
					return "NULL";
			}
			if (value > Int32.MaxValue && value <= UInt32.MaxValue)
				return ((int) ((uint) value)).ToString();
			return value.ToString();
		}
		public static string FormatHexHash(uint hash)
		{
			return $"0x{hash:X8}";
		}
		public static float SwapEndian(float num)
		{
			byte[] data = BitConverter.GetBytes(num);
			Array.Reverse(data);
			return BitConverter.ToSingle(data, 0);
		}
		public static uint SwapEndian(uint num)
		{
			byte[] data = BitConverter.GetBytes(num);
			Array.Reverse(data);
			return BitConverter.ToUInt32(data, 0);
		}
		public static int SwapEndian(int num)
		{
			byte[] data = BitConverter.GetBytes(num);
			Array.Reverse(data);
			return BitConverter.ToInt32(data, 0);
		}
		public static ulong SwapEndian(ulong num)
		{
			byte[] data = BitConverter.GetBytes(num);
			Array.Reverse(data);
			return BitConverter.ToUInt64(data, 0);
		}
		public static long SwapEndian(long num)
		{
			byte[] data = BitConverter.GetBytes(num);
			Array.Reverse(data);
			return BitConverter.ToInt64(data, 0);
		}
		public static ushort SwapEndian(ushort num)
		{
			byte[] data = BitConverter.GetBytes(num);
			Array.Reverse(data);
			return BitConverter.ToUInt16(data, 0);
		}
		public static short SwapEndian(short num)
		{
			byte[] data = BitConverter.GetBytes(num);
			Array.Reverse(data);
			return BitConverter.ToInt16(data, 0);
		}
		public static bool IntParse(string temp, out int value)
		{
			//fixes when a string push also has the same index as a function location and the decompiler adds /*func_loc*/ to the string
			if (temp.Contains("/*") && temp.Contains("*/"))
			{
				int index = temp.IndexOf("/*");
				int index2 = temp.IndexOf("*/", index + 1);
				if (index2 == -1)
				{
					value = -1;
					return false;
				} 
				temp = temp.Substring(0, index) + temp.Substring(index2 + 2);
			}
			//fixes the rare case when a string push has the same index as a known hash
			if (temp.StartsWith("joaat(\""))
			{
				temp = temp.Remove(temp.Length - 2).Substring(7);
				uint val = Joaat(temp);
				value = unchecked((int) val);
				return true;
			}
			if (Program.getIntType == Program.IntType._hex)
			{
				return int.TryParse(temp.Substring(2), System.Globalization.NumberStyles.HexNumber, new System.Globalization.CultureInfo("en-gb"), out value);
			}
			else
				return int.TryParse(temp, out value);
		}

		public static string CamelCase(string[] words)
		{
            words[0] = words[0].ToLower();

            for (int i = 1; i < words.Length; i++)
            {
                words[i] = words[i].ToLower();
                words[i] = string.Concat(words[i][0].ToString().ToUpper(), words[i].AsSpan(1));
            }

            return String.Join("", words);
        }

		public static string? TryGetLocalAutoName(string nativeStr)
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

			return null;
        }
	}
}
