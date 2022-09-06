using System;

namespace Decompiler
{
    public static class Types
	{
		public static DataTypes[] _types = new DataTypes[]
		{
			new DataTypes(Stack.DataType.Bool, 4, "BOOL", "b"),
			new DataTypes(Stack.DataType.BoolPtr, 4, "BOOL*", "pb"),
            new DataTypes(Stack.DataType.Float, 3, "float", "f"),
			new DataTypes(Stack.DataType.Int, 3, "int", "i"),
			new DataTypes(Stack.DataType.String, 3, "char[]", "c"),
			new DataTypes(Stack.DataType.StringPtr, 3, "char*", "s"),
			new DataTypes(Stack.DataType.StringPtr, 3, "const char*", "s"),
            new DataTypes(Stack.DataType.Unk, 0, "var", "u"),
			new DataTypes(Stack.DataType.Unsure, 1, "var", "u"),
			new DataTypes(Stack.DataType.IntPtr, 3, "int*", "pi"),
			new DataTypes(Stack.DataType.UnkPtr, 1, "var*", "pu"),
			new DataTypes(Stack.DataType.FloatPtr, 3, "float*", "pf"),
			new DataTypes(Stack.DataType.Vector3, 2, "Vector3", "v"),
			new DataTypes(Stack.DataType.None, 4, "void", "f"),

            new DataTypes(Stack.DataType.Any, 4, "Any", "an"),
            new DataTypes(Stack.DataType.AnyPtr, 4, "Any*", "pan"),
            new DataTypes(Stack.DataType.Blip, 4, "Blip", "bl"),
            new DataTypes(Stack.DataType.BlipPtr, 4, "Blip*", "pbl"),
            new DataTypes(Stack.DataType.Cam, 4, "Cam", "ca"),
            new DataTypes(Stack.DataType.CamPtr, 4, "Cam*", "pca"),
            new DataTypes(Stack.DataType.Entity, 4, "Entity", "e"),
            new DataTypes(Stack.DataType.EntityPtr, 4, "Entity*", "pe"),
            new DataTypes(Stack.DataType.FireId, 4, "FireId", "fi"),
            new DataTypes(Stack.DataType.FireIdPtr, 4, "FireId*", "pfi"),
            new DataTypes(Stack.DataType.Hash, 4, "Hash", "h"),
            new DataTypes(Stack.DataType.HashPtr, 4, "Hash*", "ph"),
            new DataTypes(Stack.DataType.Interior, 4, "Interior", "in"),
            new DataTypes(Stack.DataType.InteriorPtr, 4, "Interior*", "pin"),
            new DataTypes(Stack.DataType.ItemSet, 4, "ItemSet", "is"),
            new DataTypes(Stack.DataType.ItemSetPtr, 4, "ItemSet*", "pis"),
            new DataTypes(Stack.DataType.Object, 5, "Object", "o"),
            new DataTypes(Stack.DataType.ObjectPtr, 5, "Object*", "po"),
            new DataTypes(Stack.DataType.Ped, 5, "Ped", "ped"), // find a better prefix for this
            new DataTypes(Stack.DataType.PedPtr, 5, "Ped*", "pped"),
            new DataTypes(Stack.DataType.Pickup, 5, "Pickup", "pi"),
            new DataTypes(Stack.DataType.PickupPtr, 5, "Pickup*", "ppi"),
            new DataTypes(Stack.DataType.Player, 4, "Player", "pl"),
            new DataTypes(Stack.DataType.PlayerPtr, 4, "Player", "ppl"),
            new DataTypes(Stack.DataType.ScrHandle, 4, "ScrHandle", "sh"),
            new DataTypes(Stack.DataType.ScrHandlePtr, 4, "ScrHandle*", "psh"),
            new DataTypes(Stack.DataType.Vector3Ptr, 4, "Vector3*", "pv"),
            new DataTypes(Stack.DataType.Vehicle, 5, "Vehicle", "ve"),
            new DataTypes(Stack.DataType.VehiclePtr, 5, "Vehicle*", "pve"),
        };

		public static DataTypes gettype(Stack.DataType type)
		{
			foreach (DataTypes d in _types)
			{
				if (d.type == type)
					return d;
			}
			throw new Exception("Unknown type");
		}

		public static byte indexof(Stack.DataType type)
		{
			for (byte i = 0; i < _types.Length; i++)
			{
				if (_types[i].type == type)
					return i;
			}
			return 255;
		}

		public static Stack.DataType getatindex(byte index)
		{
			return _types[index].type;
		}

		public static Stack.DataType GetFromName(string name)
		{
            foreach (DataTypes d in _types)
            {
                if (d.singlename == name)
                    return d.type;
            }
            throw new Exception("Unknown type");
        }

        public static Stack.DataType GetPointerVersion(Stack.DataType type)
		{
			switch (type)
			{
				case Stack.DataType.Int:
					return Stack.DataType.IntPtr;
				case Stack.DataType.Unk:
					return Stack.DataType.UnkPtr;
				case Stack.DataType.Float:
					return Stack.DataType.FloatPtr;
				case Stack.DataType.Vector3:
					return Stack.DataType.Vector3Ptr;
				case Stack.DataType.Any:
					return Stack.DataType.AnyPtr;
				case Stack.DataType.Blip:
					return Stack.DataType.BlipPtr;
				case Stack.DataType.Cam:
					return Stack.DataType.CamPtr;
				case Stack.DataType.Entity:
					return Stack.DataType.EntityPtr;
				case Stack.DataType.FireId:
					return Stack.DataType.FireIdPtr;
				case Stack.DataType.Hash:
					return Stack.DataType.HashPtr;
				case Stack.DataType.Interior:
					return Stack.DataType.InteriorPtr;
				case Stack.DataType.ItemSet:
					return Stack.DataType.ItemSetPtr;
				case Stack.DataType.Object:
					return Stack.DataType.ObjectPtr;
				case Stack.DataType.Ped:
					return Stack.DataType.PedPtr;
				case Stack.DataType.Pickup:
					return Stack.DataType.PickupPtr;
				case Stack.DataType.Player:
					return Stack.DataType.PlayerPtr;
				case Stack.DataType.ScrHandle:
					return Stack.DataType.ScrHandlePtr;
				case Stack.DataType.Vehicle:
					return Stack.DataType.VehiclePtr;
				default:
					return type;
			}
		}

		public static bool HasPointerVersion(Stack.DataType type)
		{
			switch (type)
			{
				case Stack.DataType.Int:
				case Stack.DataType.Unk:
				case Stack.DataType.Unsure:
				case Stack.DataType.Float:
                case Stack.DataType.Vector3:
                case Stack.DataType.Any:
                case Stack.DataType.Blip:
                case Stack.DataType.Cam:
                case Stack.DataType.Entity:
                case Stack.DataType.FireId:
                case Stack.DataType.Hash:
                case Stack.DataType.Interior:
                case Stack.DataType.ItemSet:
                case Stack.DataType.Object:
                case Stack.DataType.Ped:
                case Stack.DataType.Pickup:
                case Stack.DataType.Player:
                case Stack.DataType.ScrHandle:
                case Stack.DataType.Vehicle:
                    return true;
				default:
					return false;
			}
		}

		public struct DataTypes
		{
			public Stack.DataType type;
			public int precedence;
			public string singlename;
			public string varletter;

			public DataTypes(Stack.DataType type, int precedence, string singlename, string varletter)
			{
				this.type = type;
				this.precedence = precedence;
				this.singlename = singlename;
				this.varletter = varletter;
			}

			public string returntype
			{
				get { return singlename + " "; }
			}

			public string vardec
			{
				get { return singlename + " " + varletter; }
			}

			public string vararraydec
			{
				get { return singlename + "[] " + varletter; }
			}

			public void check(DataTypes Second)
			{
				if (this.precedence < Second.precedence)
				{
					this = Second;
				}
			}
		}
	}
}
