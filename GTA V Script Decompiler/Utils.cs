using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
			}

			var typeInfo = Types.GetTypeInfo(type);

			if (typeInfo.Enum != null)
				return typeInfo.Enum.GetValue((int)value);

			if (value > int.MaxValue && value <= uint.MaxValue)
				return ((int) ((uint) value)).ToString();

			return value.ToString();
		}

		public static ushort SwapEndian(ushort num)
		{
			byte[] data = BitConverter.GetBytes(num);
			Array.Reverse(data);
			return BitConverter.ToUInt16(data, 0);
		}
	}
}
