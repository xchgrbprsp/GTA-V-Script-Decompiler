using System;

namespace Decompiler
{
    public static class Types
	{
		public static TypeInfo[] typeInfos = new TypeInfo[]
		{
			new TypeInfo(Stack.DataType.Bool, 4, "BOOL", "flag", "b"),
			new TypeInfo(Stack.DataType.BoolPtr, 4, "BOOL*", "p_bool", "pb"),
			new TypeInfo(Stack.DataType.Float, 3, "float", "num", "f"),
			new TypeInfo(Stack.DataType.Int, 3, "int", "num", "i"),
			new TypeInfo(Stack.DataType.String, 4, "char[]", "str", "c"),
			new TypeInfo(Stack.DataType.StringPtr, 4, "char*", "str", "s"),
			new TypeInfo(Stack.DataType.StringPtr, 4, "const char*", "str", "s"),
			new TypeInfo(Stack.DataType.Unk, 0, "var", "unk", "u"),
			new TypeInfo(Stack.DataType.Unsure, 1, "var", "unk", "u"),
			new TypeInfo(Stack.DataType.IntPtr, 3, "int*", "p_int", "pi"),
			new TypeInfo(Stack.DataType.UnkPtr, 1, "var*", "ptr", "p"),
			new TypeInfo(Stack.DataType.FloatPtr, 4, "float*", "p_float", "pf"),
			new TypeInfo(Stack.DataType.Vector3, 2, "Vector3", "vector", "v"),
			new TypeInfo(Stack.DataType.None, 4, "void", "???", "???"),

			new TypeInfo(Stack.DataType.Any, 2, "Any", "any", "an"),
			new TypeInfo(Stack.DataType.AnyPtr, 4, "Any*", "p_any", "pan"),
			new TypeInfo(Stack.DataType.Blip, 4, "Blip", "blip", "bl"),
			new TypeInfo(Stack.DataType.BlipPtr, 4, "Blip*", "p_blip", "pbl"),
			new TypeInfo(Stack.DataType.Cam, 4, "Cam", "cam", "ca"),
			new TypeInfo(Stack.DataType.CamPtr, 4, "Cam*", "p_cam", "pca"),
			new TypeInfo(Stack.DataType.Entity, 4, "Entity", "entity", "e"),
			new TypeInfo(Stack.DataType.EntityPtr, 4, "Entity*", "p_entity", "pe"),
			new TypeInfo(Stack.DataType.FireId, 4, "FireId", "id", "fi"),
			new TypeInfo(Stack.DataType.FireIdPtr, 4, "FireId*", "p_fireid", "pfi"),
			new TypeInfo(Stack.DataType.Hash, 4, "Hash", "hash", "h"),
			new TypeInfo(Stack.DataType.HashPtr, 4, "Hash*", "p_hash", "ph"),
			new TypeInfo(Stack.DataType.Interior, 4, "Interior", "interior", "in"),
			new TypeInfo(Stack.DataType.InteriorPtr, 4, "Interior*", "p_interior", "pin"),
			new TypeInfo(Stack.DataType.ItemSet, 4, "ItemSet", "itemset", "is"),
			new TypeInfo(Stack.DataType.ItemSetPtr, 4, "ItemSet*", "p_itemset", "pis"),
			new TypeInfo(Stack.DataType.Object, 5, "Object", "object", "ob"),
			new TypeInfo(Stack.DataType.ObjectPtr, 5, "Object*", "p_object", "pob"),
			new TypeInfo(Stack.DataType.Ped, 5, "Ped", "ped", "ped"),
            new TypeInfo(Stack.DataType.PedPtr, 5, "Ped*", "p_ped", "pped"),
			new TypeInfo(Stack.DataType.Pickup, 5, "Pickup", "pickup", "pk"),
			new TypeInfo(Stack.DataType.PickupPtr, 5, "Pickup*", "p_pickup", "pki"),
			new TypeInfo(Stack.DataType.Player, 4, "Player", "player", "pl"),
			new TypeInfo(Stack.DataType.PlayerPtr, 4, "Player", "p_player", "ppl"),
			new TypeInfo(Stack.DataType.ScrHandle, 4, "ScrHandle", "handle", "sh"),
			new TypeInfo(Stack.DataType.ScrHandlePtr, 4, "ScrHandle*", "p_handle", "psh"),
			new TypeInfo(Stack.DataType.Vector3Ptr, 4, "Vector3*", "p_vector", "pv"),
			new TypeInfo(Stack.DataType.Vehicle, 5, "Vehicle", "vehicle", "ve"),
			new TypeInfo(Stack.DataType.VehiclePtr, 5, "Vehicle*", "p_vehicle", "pve"),

			new TypeInfo(Stack.DataType.Function, 4, "function", "func", "func"),

			new TypeInfo(Stack.DataType.eControlType, 6, "eControlType", "control", "ect", new(typeof(Enums.PadControlType))),
			new TypeInfo(Stack.DataType.eControlAction, 6, "eControlAction", "action", "eca", new(typeof(Enums.PadControlAction))),
			new TypeInfo(Stack.DataType.eHudComponent, 6, "eHudComponent", "component", "ehc", new(typeof(Enums.HudComponent))),
			new TypeInfo(Stack.DataType.ePedType, 6, "ePedType", "type", "ept", new(typeof(Enums.PedType))),
			new TypeInfo(Stack.DataType.ePedComponentType, 6, "ePedComponentType", "type", "epct", new(typeof(Enums.PedComponentType))),
			new TypeInfo(Stack.DataType.eStackSize, 6, "eStackSize", "stackSize", "ess", new(typeof(Enums.StackSize))),
			new TypeInfo(Stack.DataType.eDecoratorType, 6, "eDecoratorType", "decorType", "edt", new(typeof(Enums.DecoratorType))),
			new TypeInfo(Stack.DataType.eEventGroup, 6, "eEventGroup", "group", "eeg", new(typeof(Enums.EventGroup))),
			new TypeInfo(Stack.DataType.eHudColour, 6, "eHudColour", "color", "ehc", new(typeof(Enums.HudColour))),
			new TypeInfo(Stack.DataType.eBlipSprite, 6, "eBlipSprite", "sprite", "ebs", new(typeof(Enums.BlipSprite))),
			new TypeInfo(Stack.DataType.eKnockOffVehicle, 6, "eKnockOffVehicle", "knockOffState", "eknv", new(typeof(Enums.KnockOffVehicle))),
			new TypeInfo(Stack.DataType.eCombatMovement, 6, "eCombatMovement", "movement", "ecm", new(typeof(Enums.CombatMovement))),
			new TypeInfo(Stack.DataType.eCombatAttribute, 6, "eCombatAttribute", "attribute", "ecat", new(typeof(Enums.CombatAttribute))),
			new TypeInfo(Stack.DataType.eCharacter, 6, "eCharacter", "character", "ech", new(typeof(Enums.Character))),
			new TypeInfo(Stack.DataType.eTransitionState, 6, "eTransitionState", "state", "ets", new(typeof(Enums.TransitionState))),
			new TypeInfo(Stack.DataType.eDispatchType, 6, "eDispatchType", "type", "edt", new(typeof(Enums.DispatchType))),
			new TypeInfo(Stack.DataType.eLevelIndex, 6, "eLevelIndex", "index", "eli", new(typeof(Enums.LevelIndex))),
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
			public string AutoName;
			public ScriptEnum? Enum = null;
			public string Prefix;

			public TypeInfo(Stack.DataType type, int precedence, string singlename, string autoname, string prefix, ScriptEnum? @enum = null)
			{
				this.Type = type;
				this.Precedence = precedence;
				this.SingleName = singlename;
				this.AutoName = autoname;
				this.Enum = @enum;
				this.Prefix = prefix;
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
