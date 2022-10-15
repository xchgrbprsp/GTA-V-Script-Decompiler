using System;

namespace Decompiler
{
	static class Utils
	{
		public static uint Joaat(string str)
		{
			uint hash, i;
			var key = str.ToLower().ToCharArray();
			for (hash = i = 0; i < key.Length; i++)
			{
				hash += key[i];
				hash += hash << 10;
				hash ^= hash >> 6;
			}

			hash += hash << 3;
			hash ^= hash >> 11;
			hash += hash << 15;
			return hash;
		}

		public static string Represent(long value, Types.TypeInfo type)
		{
			if (type == Types.FLOAT)
				return BitConverter.ToSingle(BitConverter.GetBytes(value), 0).ToString() + "f";

			if (type.Enum != null)
				if (type.Enum.TryGetValue((int)value, out var enumValue))
					return enumValue;

			return value is > int.MaxValue and <= uint.MaxValue ? ((int)(uint)value).ToString() : value.ToString();
		}

		public static ushort SwapEndian(ushort num)
		{
			var data = BitConverter.GetBytes(num);
			Array.Reverse(data);
			return BitConverter.ToUInt16(data, 0);
		}
	}
}
