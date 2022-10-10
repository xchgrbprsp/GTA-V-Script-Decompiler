using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler
{
    internal class NativeTypeOverride
    {
        string Name;
        int Index;
        Types.TypeInfo NewType;

        static List<NativeTypeOverride> Overrides = new();

        NativeTypeOverride(string name, int index, Types.TypeInfo newType)
        {
            Name = name;
            Index = index;
            NewType = newType;
        }

        public static void Initialize()
        {
            // PAD control type

            Overrides.Add(new("IS_CONTROL_ENABLED", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("IS_CONTROL_PRESSED", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("IS_CONTROL_RELEASED", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("IS_CONTROL_JUST_PRESSED", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("IS_CONTROL_JUST_RELEASED", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("GET_CONTROL_VALUE", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("GET_CONTROL_NORMAL", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("GET_CONTROL_UNBOUND_NORMAL", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("SET_CONTROL_VALUE_NEXT_FRAME", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("IS_DISABLED_CONTROL_PRESSED", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("IS_DISABLED_CONTROL_RELEASED", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("IS_DISABLED_CONTROL_JUST_PRESSED", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("IS_DISABLED_CONTROL_JUST_RELEASED", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("GET_DISABLED_CONTROL_NORMAL", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("GET_DISABLED_CONTROL_UNBOUND_NORMAL", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("GET_CONTROL_HOW_LONG_AGO", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("IS_USING_KEYBOARD_AND_MOUSE", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("IS_USING_CURSOR", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("IS_USING_REMOTE_PLAY", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("HAVE_CONTROLS_CHANGED", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("GET_CONTROL_INSTRUCTIONAL_BUTTONS_STRING", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("GET_CONTROL_GROUP_INSTRUCTIONAL_BUTTONS_STRING", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("SET_CONTROL_LIGHT_EFFECT_COLOR", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("CLEAR_CONTROL_LIGHT_EFFECT", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("SET_CONTROL_SHAKE", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("SET_CONTROL_TRIGGER_SHAKE", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("STOP_CONTROL_SHAKE", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("SET_CONTROL_SHAKE_SUPPRESSED_ID", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("CLEAR_CONTROL_SHAKE_SUPPRESSED_ID", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("SET_INPUT_EXCLUSIVE", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("DISABLE_CONTROL_ACTION", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("ENABLE_CONTROL_ACTION", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("DISABLE_ALL_CONTROL_ACTIONS", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("ENABLE_ALL_CONTROL_ACTIONS", 0, Types.ECONTROLTYPE));
            Overrides.Add(new("ALLOW_ALTERNATIVE_SCRIPT_CONTROLS_LAYOUT", 0, Types.ECONTROLTYPE));

            // PAD control action

            Overrides.Add(new("IS_CONTROL_ENABLED", 1, Types.ECONTROLACTION));
            Overrides.Add(new("IS_CONTROL_PRESSED", 1, Types.ECONTROLACTION));
            Overrides.Add(new("IS_CONTROL_RELEASED", 1, Types.ECONTROLACTION));
            Overrides.Add(new("IS_CONTROL_JUST_PRESSED", 1, Types.ECONTROLACTION));
            Overrides.Add(new("IS_CONTROL_JUST_RELEASED", 1, Types.ECONTROLACTION));
            Overrides.Add(new("GET_CONTROL_VALUE", 1, Types.ECONTROLACTION));
            Overrides.Add(new("GET_CONTROL_NORMAL", 1, Types.ECONTROLACTION));
            Overrides.Add(new("GET_CONTROL_UNBOUND_NORMAL", 1, Types.ECONTROLACTION));
            Overrides.Add(new("SET_CONTROL_VALUE_NEXT_FRAME", 1, Types.ECONTROLACTION));
            Overrides.Add(new("IS_DISABLED_CONTROL_PRESSED", 1, Types.ECONTROLACTION));
            Overrides.Add(new("IS_DISABLED_CONTROL_RELEASED", 1, Types.ECONTROLACTION));
            Overrides.Add(new("IS_DISABLED_CONTROL_JUST_PRESSED", 1, Types.ECONTROLACTION));
            Overrides.Add(new("IS_DISABLED_CONTROL_JUST_RELEASED", 1, Types.ECONTROLACTION));
            Overrides.Add(new("GET_DISABLED_CONTROL_NORMAL", 1, Types.ECONTROLACTION));
            Overrides.Add(new("GET_DISABLED_CONTROL_UNBOUND_NORMAL", 1, Types.ECONTROLACTION));
            Overrides.Add(new("GET_CONTROL_INSTRUCTIONAL_BUTTONS_STRING", 1, Types.ECONTROLACTION));
            Overrides.Add(new("SET_INPUT_EXCLUSIVE", 1, Types.ECONTROLACTION));
            Overrides.Add(new("DISABLE_CONTROL_ACTION", 1, Types.ECONTROLACTION));
            Overrides.Add(new("ENABLE_CONTROL_ACTION", 1, Types.ECONTROLACTION));

            // HUD, GRAPHICS hud component

            Overrides.Add(new("IS_HUD_COMPONENT_ACTIVE", 0, Types.EHUDCOMPONENT));
            Overrides.Add(new("IS_SCRIPTED_HUD_COMPONENT_ACTIVE", 0, Types.EHUDCOMPONENT));
            Overrides.Add(new("HIDE_SCRIPTED_HUD_COMPONENT_THIS_FRAME", 0, Types.EHUDCOMPONENT));
            Overrides.Add(new("SHOW_SCRIPTED_HUD_COMPONENT_THIS_FRAME", 0, Types.EHUDCOMPONENT));
            Overrides.Add(new("IS_SCRIPTED_HUD_COMPONENT_HIDDEN_THIS_FRAME", 0, Types.EHUDCOMPONENT));
            Overrides.Add(new("HIDE_HUD_COMPONENT_THIS_FRAME", 0, Types.EHUDCOMPONENT));
            Overrides.Add(new("SHOW_HUD_COMPONENT_THIS_FRAME", 0, Types.EHUDCOMPONENT));
            Overrides.Add(new("RESET_HUD_COMPONENT_VALUES", 0, Types.EHUDCOMPONENT));
            Overrides.Add(new("SET_HUD_COMPONENT_POSITION", 0, Types.EHUDCOMPONENT));
            Overrides.Add(new("GET_HUD_COMPONENT_POSITION", 0, Types.EHUDCOMPONENT));
            Overrides.Add(new("REQUEST_SCALEFORM_SCRIPT_HUD_MOVIE", 0, Types.EHUDCOMPONENT));
            Overrides.Add(new("HAS_SCALEFORM_SCRIPT_HUD_MOVIE_LOADED", 0, Types.EHUDCOMPONENT));
            Overrides.Add(new("REMOVE_SCALEFORM_SCRIPT_HUD_MOVIE", 0, Types.EHUDCOMPONENT));

            // PED ped type

            Overrides.Add(new("CREATE_PED", 0, Types.EPEDTYPE));
            Overrides.Add(new("CREATE_PED_INSIDE_VEHICLE", 1, Types.EPEDTYPE));

            // PED ped component type

            Overrides.Add(new("GET_PED_DRAWABLE_VARIATION", 1, Types.EPEDCOMPONENTTYPE));
            Overrides.Add(new("GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS", 1, Types.EPEDCOMPONENTTYPE));
            Overrides.Add(new("GET_PED_TEXTURE_VARIATION", 1, Types.EPEDCOMPONENTTYPE));
            Overrides.Add(new("GET_NUMBER_OF_PED_TEXTURE_VARIATIONS", 1, Types.EPEDCOMPONENTTYPE));
            Overrides.Add(new("GET_PED_PALETTE_VARIATION", 1, Types.EPEDCOMPONENTTYPE));
            Overrides.Add(new("IS_PED_COMPONENT_VARIATION_VALID", 1, Types.EPEDCOMPONENTTYPE));
            Overrides.Add(new("SET_PED_PRELOAD_VARIATION_DATA", 1, Types.EPEDCOMPONENTTYPE));
            Overrides.Add(new("SET_PED_PRELOAD_PROP_DATA", 1, Types.EPEDCOMPONENTTYPE));
            Overrides.Add(new("GET_PED_PROP_INDEX", 1, Types.EPEDCOMPONENTTYPE));
            Overrides.Add(new("GET_PED_PROP_TEXTURE_INDEX", 1, Types.EPEDCOMPONENTTYPE));
            Overrides.Add(new("SET_PED_COMPONENT_VARIATION", 1, Types.EPEDCOMPONENTTYPE));

            // SYSTEM, MISC stack size

            Overrides.Add(new("START_NEW_SCRIPT", 1, Types.ESTACKSIZE));
            Overrides.Add(new("START_NEW_SCRIPT_WITH_ARGS", 3, Types.ESTACKSIZE));
            Overrides.Add(new("START_NEW_SCRIPT_WITH_NAME_HASH", 1, Types.ESTACKSIZE));
            Overrides.Add(new("START_NEW_SCRIPT_WITH_NAME_HASH_AND_ARGS", 3, Types.ESTACKSIZE));
            Overrides.Add(new("GET_NUMBER_OF_FREE_STACKS_OF_THIS_SIZE", 0, Types.ESTACKSIZE));

            // DECORATOR decorator type

            Overrides.Add(new("DECOR_REGISTER", 1, Types.EDECORATORTYPE));
            Overrides.Add(new("DECOR_IS_REGISTERED_AS_TYPE", 1, Types.EDECORATORTYPE));

            // SCRIPT event group

            Overrides.Add(new("GET_NUMBER_OF_EVENTS", 0, Types.EEVENTGROUP));
            Overrides.Add(new("GET_EVENT_EXISTS", 0, Types.EEVENTGROUP));
            Overrides.Add(new("GET_EVENT_AT_INDEX", 0, Types.EEVENTGROUP));
            Overrides.Add(new("GET_EVENT_DATA", 0, Types.EEVENTGROUP));
            Overrides.Add(new("TRIGGER_SCRIPT_EVENT", 0, Types.EEVENTGROUP));
            Overrides.Add(new("SEND_TU_SCRIPT_EVENT", 0, Types.EEVENTGROUP));

            // HUD hud colour

            Overrides.Add(new("GET_HUD_COLOUR", 0, Types.EHUDCOLOUR));
            Overrides.Add(new("REPLACE_HUD_COLOUR", 0, Types.EHUDCOLOUR));
            Overrides.Add(new("REPLACE_HUD_COLOUR", 1, Types.EHUDCOLOUR));
            Overrides.Add(new("REPLACE_HUD_COLOUR_WITH_RGBA", 0, Types.EHUDCOLOUR));
            Overrides.Add(new("SET_CUSTOM_MP_HUD_COLOR", 0, Types.EHUDCOLOUR));
            Overrides.Add(new("SET_COLOUR_OF_NEXT_TEXT_COMPONENT", 0, Types.EHUDCOLOUR));

            // HUD blip sprites

            Overrides.Add(new("GET_NEXT_BLIP_INFO_ID", 0, Types.EBLIPSPRITE));
            Overrides.Add(new("GET_FIRST_BLIP_INFO_ID", 0, Types.EBLIPSPRITE));
            Overrides.Add(new("GET_CLOSEST_BLIP_INFO_ID", 0, Types.EBLIPSPRITE));
            Overrides.Add(new("SET_BLIP_SPRITE", 1, Types.EBLIPSPRITE));
            Overrides.Add(new("GET_BLIP_SPRITE", -1, Types.EBLIPSPRITE));
            Overrides.Add(new("CUSTOM_MINIMAP_SET_BLIP_OBJECT", 0, Types.EBLIPSPRITE));

            // PED knockoff vehicle

            Overrides.Add(new("SET_PED_CAN_BE_KNOCKED_OFF_VEHICLE", 1, Types.EKNOCKOFFVEHICLE));

            // PED combat movement

            Overrides.Add(new("SET_PED_COMBAT_MOVEMENT", 1, Types.ECOMBATMOVEMENT));

            // PED combat attribute

            Overrides.Add(new("SET_PED_COMBAT_ATTRIBUTES", 1, Types.ECOMBATATTRIBUTE));

            // MISC dispatch type

            Overrides.Add(new("ENABLE_DISPATCH_SERVICE", 0, Types.EDISPATCHTYPE));
            Overrides.Add(new("BLOCK_DISPATCH_SERVICE_RESOURCE_CREATION", 0, Types.EDISPATCHTYPE));
            Overrides.Add(new("GET_NUMBER_RESOURCES_ALLOCATED_TO_WANTED_LEVEL", 0, Types.EDISPATCHTYPE));
            Overrides.Add(new("CREATE_INCIDENT", 0, Types.EDISPATCHTYPE));
            Overrides.Add(new("CREATE_INCIDENT_WITH_ENTITY", 0, Types.EDISPATCHTYPE));
            Overrides.Add(new("SET_INCIDENT_REQUESTED_UNITS", 1, Types.EDISPATCHTYPE));

            // MISC level index

            Overrides.Add(new("GET_INDEX_OF_CURRENT_LEVEL", -1, Types.ELEVELINDEX));

            // CAM view mode context

            Overrides.Add(new("GET_CAM_ACTIVE_VIEW_MODE_CONTEXT", -1, Types.EVIEWMODECONTEXT));
            Overrides.Add(new("GET_CAM_VIEW_MODE_FOR_CONTEXT", 0, Types.EVIEWMODECONTEXT));
            Overrides.Add(new("SET_CAM_VIEW_MODE_FOR_CONTEXT", 0, Types.EVIEWMODECONTEXT));

            // SYSTEM thread priority

            Overrides.Add(new("SET_THIS_THREAD_PRIORITY", 0, Types.ETHREADPRIORITY));

            // PLAYER set player control flags

            Overrides.Add(new("SET_PLAYER_CONTROL", 2, Types.ESETPLAYERCONTROLFLAGS));

            // TASK script lookat flags

            Overrides.Add(new("TASK_LOOK_AT_COORD", 5, Types.ESCRIPTLOOKATFLAGS));
            Overrides.Add(new("TASK_LOOK_AT_ENTITY", 3, Types.ESCRIPTLOOKATFLAGS));

            // TASK script task hash

            Overrides.Add(new("GET_SCRIPT_TASK_STATUS", 1, Types.ESCRIPTTASKHASH));

            // EVENT, SCRIPT event type

            Overrides.Add(new("CLEAR_DECISION_MAKER_EVENT_RESPONSE", 1, Types.EEVENTTYPE));
            Overrides.Add(new("BLOCK_DECISION_MAKER_EVENT", 1, Types.EEVENTTYPE));
            Overrides.Add(new("UNBLOCK_DECISION_MAKER_EVENT", 1, Types.EEVENTTYPE));
            Overrides.Add(new("ADD_SHOCKING_EVENT_AT_POSITION", 0, Types.EEVENTTYPE));
            Overrides.Add(new("ADD_SHOCKING_EVENT_FOR_ENTITY", 0, Types.EEVENTTYPE));
            Overrides.Add(new("IS_SHOCKING_EVENT_IN_SPHERE", 0, Types.EEVENTTYPE));
            Overrides.Add(new("SUPPRESS_SHOCKING_EVENT_TYPE_NEXT_FRAME", 0, Types.EEVENTTYPE));
            Overrides.Add(new("GET_EVENT_AT_INDEX", -1, Types.EEVENTTYPE));
        }

        public static void Visit(ref NativeDBEntry entry)
        {
            foreach (var ovr in Overrides)
                if (ovr.Name == entry.name)
                {
                    if (ovr.Index == -1)
                        entry.SetReturnType(ovr.NewType);
                    else
                        entry.SetParamType(ovr.Index, ovr.NewType);
                }
        }
    }
}
