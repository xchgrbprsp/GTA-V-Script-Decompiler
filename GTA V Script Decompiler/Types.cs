using System;
using System.Collections.Generic;

namespace Decompiler
{
    public static class Types
    {
        public static readonly List<TypeInfo> typeInfos = new();

        public static readonly TypeInfo BOOL = new(Stack.DataType.Bool, 4, "BOOL", "flag", "b");
        public static readonly TypeInfo PBOOL = new(Stack.DataType.BoolPtr, 4, "BOOL*", "p_bool", "pb");
        public static readonly TypeInfo FLOAT = new(Stack.DataType.Float, 3, "float", "num", "f");
        public static readonly TypeInfo INT = new(Stack.DataType.Int, 3, "int", "num", "i");
        public static readonly TypeInfo STRING = new(Stack.DataType.String, 4, "char[]", "str", "c");
        public static readonly TypeInfo PSTRING = new(Stack.DataType.StringPtr, 4, "char*", "str", "s");
        public static readonly TypeInfo CPSTRING = new(Stack.DataType.StringPtr, 4, "const char*", "str", "s");
        public static readonly TypeInfo UNKNOWN = new(Stack.DataType.Unk, 0, "var", "unk", "u");
        public static readonly TypeInfo PINT = new(Stack.DataType.IntPtr, 3, "int*", "p_int", "pi");
        public static readonly TypeInfo PUNKNOWN = new(Stack.DataType.UnkPtr, 1, "var*", "ptr", "p");
        public static readonly TypeInfo PFLOAT = new(Stack.DataType.FloatPtr, 4, "float*", "p_float", "pf");
        public static readonly TypeInfo VEC3 = new(Stack.DataType.Vector3, 2, "Vector3", "vector", "v");
        public static readonly TypeInfo VOID = new(Stack.DataType.None, 4, "void", "???", "???");

        public static readonly TypeInfo ANY = new(Stack.DataType.Any, 2, "Any", "any", "an");
        public static readonly TypeInfo PANY = new(Stack.DataType.AnyPtr, 4, "Any*", "p_any", "pan");
        public static readonly TypeInfo BLIP = new(Stack.DataType.Blip, 4, "Blip", "blip", "bl");
        public static readonly TypeInfo PBLIP = new(Stack.DataType.BlipPtr, 4, "Blip*", "p_blip", "pbl");
        public static readonly TypeInfo CAM = new(Stack.DataType.Cam, 4, "Cam", "cam", "ca");
        public static readonly TypeInfo PCAM = new(Stack.DataType.CamPtr, 4, "Cam*", "p_cam", "pca");
        public static readonly TypeInfo ENTITY = new(Stack.DataType.Entity, 4, "Entity", "entity", "e");
        public static readonly TypeInfo PENTITY = new(Stack.DataType.EntityPtr, 4, "Entity*", "p_entity", "pe");
        public static readonly TypeInfo FIREID = new(Stack.DataType.FireId, 4, "FireId", "id", "fi");
        public static readonly TypeInfo PFIREID = new(Stack.DataType.FireIdPtr, 4, "FireId*", "p_fireid", "pfi");
        public static readonly TypeInfo HASH = new(Stack.DataType.Hash, 4, "Hash", "hash", "h");
        public static readonly TypeInfo PHASH = new(Stack.DataType.HashPtr, 4, "Hash*", "p_hash", "ph");
        public static readonly TypeInfo INTERIOR = new(Stack.DataType.Interior, 4, "Interior", "interior", "in");
        public static readonly TypeInfo PINTERIOR = new(Stack.DataType.InteriorPtr, 4, "Interior*", "p_interior", "pin");
        public static readonly TypeInfo ITEMSET = new(Stack.DataType.ItemSet, 4, "ItemSet", "itemset", "is");
        public static readonly TypeInfo PITEMSET = new(Stack.DataType.ItemSetPtr, 4, "ItemSet*", "p_itemset", "pis");
        public static readonly TypeInfo OBJECT = new(Stack.DataType.Object, 5, "Object", "object", "ob");
        public static readonly TypeInfo POBJECT = new(Stack.DataType.ObjectPtr, 5, "Object*", "p_object", "pob");
        public static readonly TypeInfo PED = new(Stack.DataType.Ped, 5, "Ped", "ped", "ped");
        public static readonly TypeInfo PPED = new(Stack.DataType.PedPtr, 5, "Ped*", "p_ped", "pped");
        public static readonly TypeInfo PICKUP = new(Stack.DataType.Pickup, 5, "Pickup", "pickup", "pk");
        public static readonly TypeInfo PPICKUP = new(Stack.DataType.PickupPtr, 5, "Pickup*", "p_pickup", "pki");
        public static readonly TypeInfo PLAYER = new(Stack.DataType.Player, 4, "Player", "player", "pl");
        public static readonly TypeInfo PPLAYER = new(Stack.DataType.PlayerPtr, 4, "Player", "p_player", "ppl");
        public static readonly TypeInfo SCRHANDLE = new(Stack.DataType.ScrHandle, 4, "ScrHandle", "handle", "sh");
        public static readonly TypeInfo PSCRHANDLE = new(Stack.DataType.ScrHandlePtr, 4, "ScrHandle*", "p_handle", "psh");
        public static readonly TypeInfo PVEC3 = new(Stack.DataType.Vector3Ptr, 4, "Vector3*", "p_vector", "pv");
        public static readonly TypeInfo VEHICLE = new(Stack.DataType.Vehicle, 5, "Vehicle", "vehicle", "ve");
        public static readonly TypeInfo PVEHICLE = new(Stack.DataType.VehiclePtr, 5, "Vehicle*", "p_vehicle", "pve");
        public static readonly TypeInfo FUNCTION = new(Stack.DataType.Function, 5, "function", "func", "func");

        public static readonly TypeInfo ECONTROLTYPE = new(Stack.DataType.eControlType, 6, "eControlType", "control", "ect", new(typeof(Enums.PadControlType)));
        public static readonly TypeInfo ECONTROLACTION = new(Stack.DataType.eControlAction, 6, "eControlAction", "action", "eca", new(typeof(Enums.PadControlAction)));
        public static readonly TypeInfo EHUDCOMPONENT = new(Stack.DataType.eHudComponent, 6, "eHudComponent", "component", "ehc", new(typeof(Enums.HudComponent)));
        public static readonly TypeInfo EPEDTYPE = new(Stack.DataType.ePedType, 6, "ePedType", "type", "ept", new(typeof(Enums.PedType)));
        public static readonly TypeInfo EPEDCOMPONENTTYPE = new(Stack.DataType.ePedComponentType, 6, "ePedComponentType", "type", "epct", new(typeof(Enums.PedComponentType)));
        public static readonly TypeInfo ESTACKSIZE = new(Stack.DataType.eStackSize, 6, "eStackSize", "stackSize", "ess", new(typeof(Enums.StackSize)));
        public static readonly TypeInfo EDECORATORTYPE = new(Stack.DataType.eDecoratorType, 6, "eDecoratorType", "decorType", "edt", new(typeof(Enums.DecoratorType)));
        public static readonly TypeInfo EEVENTGROUP = new(Stack.DataType.eEventGroup, 6, "eEventGroup", "group", "eeg", new(typeof(Enums.EventGroup)));
        public static readonly TypeInfo EHUDCOLOUR = new(Stack.DataType.eHudColour, 6, "eHudColour", "color", "ehc", new(typeof(Enums.HudColour)));
        public static readonly TypeInfo EBLIPSPRITE = new(Stack.DataType.eBlipSprite, 6, "eBlipSprite", "sprite", "ebs", new(typeof(Enums.BlipSprite)));
        public static readonly TypeInfo EKNOCKOFFVEHICLE = new(Stack.DataType.eKnockOffVehicle, 6, "eKnockOffVehicle", "knockOffState", "eknv", new(typeof(Enums.KnockOffVehicle)));
        public static readonly TypeInfo ECOMBATMOVEMENT = new(Stack.DataType.eCombatMovement, 6, "eCombatMovement", "movement", "ecm", new(typeof(Enums.CombatMovement)));
        public static readonly TypeInfo ECOMBATATTRIBUTE = new(Stack.DataType.eCombatAttribute, 6, "eCombatAttribute", "attribute", "ecat", new(typeof(Enums.CombatAttribute)));
        public static readonly TypeInfo ECHARACTER = new(Stack.DataType.eCharacter, 6, "eCharacter", "character", "ech", new(typeof(Enums.Character)));
        public static readonly TypeInfo ETRANSITIONSTATE = new(Stack.DataType.eTransitionState, 6, "eTransitionState", "state", "ets", new(typeof(Enums.TransitionState)));
        public static readonly TypeInfo EDISPATCHTYPE = new(Stack.DataType.eDispatchType, 6, "eDispatchType", "type", "edt", new(typeof(Enums.DispatchType)));
        public static readonly TypeInfo ELEVELINDEX = new(Stack.DataType.eLevelIndex, 6, "eLevelIndex", "index", "eli", new(typeof(Enums.LevelIndex)));
        public static readonly TypeInfo EVIEWMODECONTEXT = new(Stack.DataType.eViewModeContext, 6, "eViewModeContext", "context", "evmc", new(typeof(Enums.ViewModeContext)));
        public static readonly TypeInfo ETHREADPRIORITY = new(Stack.DataType.eThreadPriority, 6, "eThreadPriority", "priority", "etp", new(typeof(Enums.ThreadPriority)));
        public static readonly TypeInfo ESETPLAYERCONTROLFLAGS = new(Stack.DataType.eSetPlayerControlFlags, 6, "eSetPlayerControlFlags", "controlBs", "espcf", new(typeof(Enums.SetPlayerControlFlag), true));
        public static readonly TypeInfo ESCRIPTLOOKATFLAGS = new(Stack.DataType.eScriptLookAtFlags, 6, "eScriptLookAtFlags", "lookatFlags", "eslf", new(typeof(Enums.ScriptLookatFlags), true));
        public static readonly TypeInfo ESCRIPTTASKHASH = new(Stack.DataType.eScriptTaskHash, 6, "eScriptTaskHash", "taskHash", "esth", new(typeof(Enums.ScriptTaskHash)));
        public static readonly TypeInfo EMPSTAT = new(Stack.DataType.eMPStat, 6, "eMPStat", "stat", "emps", new(typeof(Enums.MPStat)));
        public static readonly TypeInfo EEVENTTYPE = new(Stack.DataType.eEventType, 6, "eEventType", "type", "eet", new(typeof(Enums.EventType)));

        public static TypeInfo GetTypeInfo(Stack.DataType type)
        {
            foreach (TypeInfo d in typeInfos)
            {
                if (d.Type == type)
                    return d;
            }
            throw new Exception("Unknown type");
        }

        public static TypeInfo GetFromName(string name)
        {
            foreach (TypeInfo d in typeInfos)
            {
                if (d.SingleName.ToLower() == name.ToLower())
                    return d;
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

        public static Stack.DataType GetLiteralVersion(Stack.DataType type)
        {
            switch (type)
            {
                case Stack.DataType.IntPtr:
                    return Stack.DataType.Int;
                case Stack.DataType.UnkPtr:
                    return Stack.DataType.Unk;
                case Stack.DataType.BoolPtr:
                    return Stack.DataType.Bool;
                case Stack.DataType.FloatPtr:
                    return Stack.DataType.Float;
                case Stack.DataType.Vector3Ptr:
                    return Stack.DataType.Vector3;
                case Stack.DataType.AnyPtr:
                    return Stack.DataType.Any;
                case Stack.DataType.BlipPtr:
                    return Stack.DataType.Blip;
                case Stack.DataType.CamPtr:
                    return Stack.DataType.Cam;
                case Stack.DataType.EntityPtr:
                    return Stack.DataType.Entity;
                case Stack.DataType.FireIdPtr:
                    return Stack.DataType.FireId;
                case Stack.DataType.HashPtr:
                    return Stack.DataType.Hash;
                case Stack.DataType.InteriorPtr:
                    return Stack.DataType.Interior;
                case Stack.DataType.ItemSetPtr:
                    return Stack.DataType.ItemSet;
                case Stack.DataType.ObjectPtr:
                    return Stack.DataType.Object;
                case Stack.DataType.PedPtr:
                    return Stack.DataType.Ped;
                case Stack.DataType.PickupPtr:
                    return Stack.DataType.Pickup;
                case Stack.DataType.PlayerPtr:
                    return Stack.DataType.Player;
                case Stack.DataType.ScrHandlePtr:
                    return Stack.DataType.ScrHandle;
                case Stack.DataType.VehiclePtr:
                    return Stack.DataType.Vehicle;
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

        public static bool HasLiteralVersion(Stack.DataType type)
        {
            switch (type)
            {
                case Stack.DataType.IntPtr:
                case Stack.DataType.UnkPtr:
                case Stack.DataType.BoolPtr:
                case Stack.DataType.FloatPtr:
                case Stack.DataType.Vector3Ptr:
                case Stack.DataType.AnyPtr:
                case Stack.DataType.BlipPtr:
                case Stack.DataType.CamPtr:
                case Stack.DataType.EntityPtr:
                case Stack.DataType.FireIdPtr:
                case Stack.DataType.HashPtr:
                case Stack.DataType.InteriorPtr:
                case Stack.DataType.ItemSetPtr:
                case Stack.DataType.ObjectPtr:
                case Stack.DataType.PedPtr:
                case Stack.DataType.PickupPtr:
                case Stack.DataType.PlayerPtr:
                case Stack.DataType.ScrHandlePtr:
                case Stack.DataType.VehiclePtr:
                    return true;
                default:
                    return false;
            }
        }

        public class TypeInfo
        {
            public Stack.DataType Type;
            public int Precedence;
            public string SingleName;
            public string AutoName;
            public ScriptEnum? Enum = null;
            public string Prefix;
            TypeContainer sealedContainer;

            public TypeInfo(Stack.DataType type, int precedence, string singlename, string autoname, string prefix, ScriptEnum? @enum = null)
            {
                this.Type = type;
                this.Precedence = precedence;
                this.SingleName = singlename;
                this.AutoName = autoname;
                this.Enum = @enum;
                this.Prefix = prefix;

                Types.typeInfos.Add(this);
                sealedContainer = new(this, true);
            }

            public string ReturnType
            {
                get { return SingleName + " "; }
            }

            public string VarDec
            {
                get { return SingleName + " "; }
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

            /// <summary>
            /// Gets a reference to a <b>SEALED</b> container containing the type
            /// </summary>
            public ref TypeContainer GetContainer()
            {
                return ref sealedContainer;
            }
        }
    }

    /// <summary>
    /// A "container" to hold a type. The container will attempt to propagate itself throught the AST and can even jump across functions
    /// </summary>
    public class TypeContainer
    {
        public Types.TypeInfo Type { get; private set; }
        bool IsSealed;

        public TypeContainer(Types.TypeInfo defaultType = null, bool isSealed = false)
        {
            Type = defaultType ?? Types.UNKNOWN;
            IsSealed = isSealed;
        }

        public void HintType(Types.TypeInfo type)
        {
            if (IsSealed)
                return;

            if (Type < type)
                Type = type;
        }

        /// <summary>
        /// The reference passed WILL be overwritten unless it is sealed
        /// </summary>
        public void HintType(ref TypeContainer container)
        {
            if ((!IsSealed) && Type < container.Type)
                Type = container.Type;

            else if (IsSealed && (!container.IsSealed) && Type > container.Type)
                container.Type = Type;

            if (!(container.IsSealed || IsSealed))
                container = this;
        }

        /// <summary>
        /// Sealing a container makes the underlying type constant and stops propagation
        /// </summary>
        public void SealType()
        {
            IsSealed = true;
        }
    }
}
