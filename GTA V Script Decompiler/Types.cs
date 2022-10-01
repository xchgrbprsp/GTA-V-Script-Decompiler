using System;

namespace Decompiler
{
    public static class Types
	{
		public static TypeInfo[] typeInfos = new TypeInfo[]
		{
			new TypeInfo(Stack.DataType.Bool, 4, "BOOL", "b"),
			new TypeInfo(Stack.DataType.BoolPtr, 4, "BOOL*", "pb"),
			new TypeInfo(Stack.DataType.Float, 3, "float", "f"),
			new TypeInfo(Stack.DataType.Int, 3, "int", "i"),
			new TypeInfo(Stack.DataType.String, 3, "char[]", "c"),
			new TypeInfo(Stack.DataType.StringPtr, 3, "char*", "s"),
			new TypeInfo(Stack.DataType.StringPtr, 3, "const char*", "s"),
			new TypeInfo(Stack.DataType.Unk, 0, "var", "u"),
			new TypeInfo(Stack.DataType.Unsure, 1, "var", "u"),
			new TypeInfo(Stack.DataType.IntPtr, 3, "int*", "pi"),
			new TypeInfo(Stack.DataType.UnkPtr, 1, "var*", "pu"),
			new TypeInfo(Stack.DataType.FloatPtr, 4, "float*", "pf"),
			new TypeInfo(Stack.DataType.Vector3, 2, "Vector3", "v"),
			new TypeInfo(Stack.DataType.None, 4, "void", "f"),

			new TypeInfo(Stack.DataType.Any, 4, "Any", "an"),
			new TypeInfo(Stack.DataType.AnyPtr, 4, "Any*", "pan"),
			new TypeInfo(Stack.DataType.Blip, 4, "Blip", "bl"),
			new TypeInfo(Stack.DataType.BlipPtr, 4, "Blip*", "pbl"),
			new TypeInfo(Stack.DataType.Cam, 4, "Cam", "ca"),
			new TypeInfo(Stack.DataType.CamPtr, 4, "Cam*", "pca"),
			new TypeInfo(Stack.DataType.Entity, 4, "Entity", "e"),
			new TypeInfo(Stack.DataType.EntityPtr, 4, "Entity*", "pe"),
			new TypeInfo(Stack.DataType.FireId, 4, "FireId", "fi"),
			new TypeInfo(Stack.DataType.FireIdPtr, 4, "FireId*", "pfi"),
			new TypeInfo(Stack.DataType.Hash, 4, "Hash", "h"),
			new TypeInfo(Stack.DataType.HashPtr, 4, "Hash*", "ph"),
			new TypeInfo(Stack.DataType.Interior, 4, "Interior", "in"),
			new TypeInfo(Stack.DataType.InteriorPtr, 4, "Interior*", "pin"),
			new TypeInfo(Stack.DataType.ItemSet, 4, "ItemSet", "is"),
			new TypeInfo(Stack.DataType.ItemSetPtr, 4, "ItemSet*", "pis"),
			new TypeInfo(Stack.DataType.Object, 5, "Object", "o"),
			new TypeInfo(Stack.DataType.ObjectPtr, 5, "Object*", "po"),
			new TypeInfo(Stack.DataType.Ped, 5, "Ped", "ped"), // find a better prefix for this
            new TypeInfo(Stack.DataType.PedPtr, 5, "Ped*", "pped"),
			new TypeInfo(Stack.DataType.Pickup, 5, "Pickup", "pk"),
			new TypeInfo(Stack.DataType.PickupPtr, 5, "Pickup*", "pki"),
			new TypeInfo(Stack.DataType.Player, 4, "Player", "pl"),
			new TypeInfo(Stack.DataType.PlayerPtr, 4, "Player", "ppl"),
			new TypeInfo(Stack.DataType.ScrHandle, 4, "ScrHandle", "sh"),
			new TypeInfo(Stack.DataType.ScrHandlePtr, 4, "ScrHandle*", "psh"),
			new TypeInfo(Stack.DataType.Vector3Ptr, 4, "Vector3*", "pv"),
			new TypeInfo(Stack.DataType.Vehicle, 5, "Vehicle", "ve"),
			new TypeInfo(Stack.DataType.VehiclePtr, 5, "Vehicle*", "pve"),

			new TypeInfo(Stack.DataType.Function, 4, "function", "func"),

			new TypeInfo(Stack.DataType.eControlType, 6, "eControlType", "ect", new(typeof(Enums.PadControlType))),
			new TypeInfo(Stack.DataType.eControlAction, 6, "eControlAction", "eca", new(typeof(Enums.PadControlAction))),
			new TypeInfo(Stack.DataType.eHudComponent, 6, "eHudComponent", "ehcom", new(typeof(Enums.HudComponent))),
			new TypeInfo(Stack.DataType.ePedType, 6, "ePedType", "ept", new(typeof(Enums.PedType))),
			new TypeInfo(Stack.DataType.ePedComponentType, 6, "ePedComponentType", "epct", new(typeof(Enums.PedComponentType))),
			new TypeInfo(Stack.DataType.eStackSize, 6, "eStackSize", "ess", new(typeof(Enums.StackSize))),
			new TypeInfo(Stack.DataType.eDecoratorType, 6, "eDecoratorType", "edt", new(typeof(Enums.DecoratorType))),
			new TypeInfo(Stack.DataType.eEventGroup, 6, "eEventGroup", "eeg", new(typeof(Enums.EventGroup))),
			new TypeInfo(Stack.DataType.eHudColour, 6, "eHudColour", "ehcol", new(typeof(Enums.HudColour))),
			new TypeInfo(Stack.DataType.eBlipSprite, 6, "eBlipSprite", "ebs", new(typeof(Enums.BlipSprite))),
			new TypeInfo(Stack.DataType.eKnockOffVehicle, 6, "eKnockOffVehicle", "ekv", new(typeof(Enums.KnockOffVehicle))),
			new TypeInfo(Stack.DataType.eCombatMovement, 6, "eCombatMovement", "ecm", new(typeof(Enums.CombatMovement))),
			new TypeInfo(Stack.DataType.eCombatAttribute, 6, "eCombatAttribute", "ecat", new(typeof(Enums.CombatAttribute))),
			new TypeInfo(Stack.DataType.eCharacter, 6, "eCharacter", "ech", new(typeof(Enums.Character))),
			new TypeInfo(Stack.DataType.eTransitionState, 6, "eTransitionState", "ets", new(typeof(Enums.TransitionState))),
        };

		public static TypeInfo GetTypeInfo(Stack.DataType type)
		{
			foreach (TypeInfo d in typeInfos)
			{
				if (d.Type == type)
					return d;
			}
			throw new Exception("Unknown type");
		}

		public static byte IndexOf(Stack.DataType type)
		{
			for (byte i = 0; i < typeInfos.Length; i++)
			{
				if (typeInfos[i].Type == type)
					return i;
			}
			return 255;
		}

		public static Stack.DataType GetAtIndex(byte index)
		{
			return typeInfos[index].Type;
		}

		public static Stack.DataType GetFromName(string name)
		{
            foreach (TypeInfo d in typeInfos)
            {
                if (d.SingleName.ToLower() == name.ToLower())
                    return d.Type;
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
				case Stack.DataType.Bool:
					return Stack.DataType.BoolPtr;
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
                case Stack.DataType.Bool:
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

		public static Stack.DataType GetPrecise(Stack.DataType t1, Stack.DataType t2)
		{
			var t1info = GetTypeInfo(t1);
			var t2info = GetTypeInfo(t2);

			return t1info > t2info ? t1 : t2;
        }

		public struct TypeInfo
		{
			public Stack.DataType Type;
			public int Precedence;
			public string SingleName;
			public string Prefix;
			public ScriptEnum? Enum = null;

			public TypeInfo(Stack.DataType type, int precedence, string singlename, string varletter, ScriptEnum? @enum = null)
			{
				this.Type = type;
				this.Precedence = precedence;
				this.SingleName = singlename;
				this.Prefix = varletter;
				this.Enum = @enum;
			}

			public string ReturnType
			{
				get { return SingleName + " "; }
			}

			public string VarDec
			{
				get { return SingleName + " " ; }
			}

			public string ArrayDec
			{
				get { return SingleName + "[] "; }
			}

            public static bool operator >(TypeInfo a, TypeInfo b)
            {
				return a.Precedence > b.Precedence;
            }

            public static bool operator <(TypeInfo a, TypeInfo b)
            {
                return a.Precedence < b.Precedence;
            }
        }
	}
}
