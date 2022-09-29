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
        Stack.DataType NewType;

        static List<NativeTypeOverride> Overrides = new List<NativeTypeOverride>();

        NativeTypeOverride(string name, int index, Stack.DataType newType)
        {
            Name = name;
            Index = index;
            NewType = newType;
        }

        public static void Initialize()
        {
            // PAD control type

            Overrides.Add(new("IS_CONTROL_ENABLED", 0, Stack.DataType.eControlType));
            Overrides.Add(new("IS_CONTROL_PRESSED", 0, Stack.DataType.eControlType));
            Overrides.Add(new("IS_CONTROL_RELEASED", 0, Stack.DataType.eControlType));
            Overrides.Add(new("IS_CONTROL_JUST_PRESSED", 0, Stack.DataType.eControlType));
            Overrides.Add(new("IS_CONTROL_JUST_RELEASED", 0, Stack.DataType.eControlType));
            Overrides.Add(new("GET_CONTROL_VALUE", 0, Stack.DataType.eControlType));
            Overrides.Add(new("GET_CONTROL_NORMAL", 0, Stack.DataType.eControlType));
            Overrides.Add(new("GET_CONTROL_UNBOUND_NORMAL", 0, Stack.DataType.eControlType));
            Overrides.Add(new("SET_CONTROL_VALUE_NEXT_FRAME", 0, Stack.DataType.eControlType));
            Overrides.Add(new("IS_DISABLED_CONTROL_PRESSED", 0, Stack.DataType.eControlType));
            Overrides.Add(new("IS_DISABLED_CONTROL_RELEASED", 0, Stack.DataType.eControlType));
            Overrides.Add(new("IS_DISABLED_CONTROL_JUST_PRESSED", 0, Stack.DataType.eControlType));
            Overrides.Add(new("IS_DISABLED_CONTROL_JUST_RELEASED", 0, Stack.DataType.eControlType));
            Overrides.Add(new("GET_DISABLED_CONTROL_NORMAL", 0, Stack.DataType.eControlType));
            Overrides.Add(new("GET_DISABLED_CONTROL_UNBOUND_NORMAL", 0, Stack.DataType.eControlType));
            Overrides.Add(new("GET_CONTROL_HOW_LONG_AGO", 0, Stack.DataType.eControlType));
            Overrides.Add(new("IS_USING_KEYBOARD_AND_MOUSE", 0, Stack.DataType.eControlType));
            Overrides.Add(new("IS_USING_CURSOR", 0, Stack.DataType.eControlType));
            Overrides.Add(new("IS_USING_REMOTE_PLAY", 0, Stack.DataType.eControlType));
            Overrides.Add(new("HAVE_CONTROLS_CHANGED", 0, Stack.DataType.eControlType));
            Overrides.Add(new("GET_CONTROL_INSTRUCTIONAL_BUTTONS_STRING", 0, Stack.DataType.eControlType));
            Overrides.Add(new("GET_CONTROL_GROUP_INSTRUCTIONAL_BUTTONS_STRING", 0, Stack.DataType.eControlType));
            Overrides.Add(new("SET_CONTROL_LIGHT_EFFECT_COLOR", 0, Stack.DataType.eControlType));
            Overrides.Add(new("CLEAR_CONTROL_LIGHT_EFFECT", 0, Stack.DataType.eControlType));
            Overrides.Add(new("SET_CONTROL_SHAKE", 0, Stack.DataType.eControlType));
            Overrides.Add(new("SET_CONTROL_TRIGGER_SHAKE", 0, Stack.DataType.eControlType));
            Overrides.Add(new("STOP_CONTROL_SHAKE", 0, Stack.DataType.eControlType));
            Overrides.Add(new("SET_CONTROL_SHAKE_SUPPRESSED_ID", 0, Stack.DataType.eControlType));
            Overrides.Add(new("CLEAR_CONTROL_SHAKE_SUPPRESSED_ID", 0, Stack.DataType.eControlType));
            Overrides.Add(new("SET_INPUT_EXCLUSIVE", 0, Stack.DataType.eControlType));
            Overrides.Add(new("DISABLE_CONTROL_ACTION", 0, Stack.DataType.eControlType));
            Overrides.Add(new("ENABLE_CONTROL_ACTION", 0, Stack.DataType.eControlType));
            Overrides.Add(new("DISABLE_ALL_CONTROL_ACTIONS", 0, Stack.DataType.eControlType));
            Overrides.Add(new("ENABLE_ALL_CONTROL_ACTIONS", 0, Stack.DataType.eControlType));
            Overrides.Add(new("ALLOW_ALTERNATIVE_SCRIPT_CONTROLS_LAYOUT", 0, Stack.DataType.eControlType));

            // PAD control action

            Overrides.Add(new("IS_CONTROL_ENABLED", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("IS_CONTROL_PRESSED", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("IS_CONTROL_RELEASED", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("IS_CONTROL_JUST_PRESSED", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("IS_CONTROL_JUST_RELEASED", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("GET_CONTROL_VALUE", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("GET_CONTROL_NORMAL", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("GET_CONTROL_UNBOUND_NORMAL", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("SET_CONTROL_VALUE_NEXT_FRAME", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("IS_DISABLED_CONTROL_PRESSED", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("IS_DISABLED_CONTROL_RELEASED", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("IS_DISABLED_CONTROL_JUST_PRESSED", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("IS_DISABLED_CONTROL_JUST_RELEASED", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("GET_DISABLED_CONTROL_NORMAL", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("GET_DISABLED_CONTROL_UNBOUND_NORMAL", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("GET_CONTROL_INSTRUCTIONAL_BUTTONS_STRING", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("SET_INPUT_EXCLUSIVE", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("DISABLE_CONTROL_ACTION", 1, Stack.DataType.eControlAction));
            Overrides.Add(new("ENABLE_CONTROL_ACTION", 1, Stack.DataType.eControlAction));

            // HUD, GRAPHICS hud component

            Overrides.Add(new("IS_HUD_COMPONENT_ACTIVE", 0, Stack.DataType.eHudComponent));
            Overrides.Add(new("IS_SCRIPTED_HUD_COMPONENT_ACTIVE", 0, Stack.DataType.eHudComponent));
            Overrides.Add(new("HIDE_SCRIPTED_HUD_COMPONENT_THIS_FRAME", 0, Stack.DataType.eHudComponent));
            Overrides.Add(new("SHOW_SCRIPTED_HUD_COMPONENT_THIS_FRAME", 0, Stack.DataType.eHudComponent));
            Overrides.Add(new("IS_SCRIPTED_HUD_COMPONENT_HIDDEN_THIS_FRAME", 0, Stack.DataType.eHudComponent));
            Overrides.Add(new("HIDE_HUD_COMPONENT_THIS_FRAME", 0, Stack.DataType.eHudComponent));
            Overrides.Add(new("SHOW_HUD_COMPONENT_THIS_FRAME", 0, Stack.DataType.eHudComponent));
            Overrides.Add(new("RESET_HUD_COMPONENT_VALUES", 0, Stack.DataType.eHudComponent));
            Overrides.Add(new("SET_HUD_COMPONENT_POSITION", 0, Stack.DataType.eHudComponent));
            Overrides.Add(new("GET_HUD_COMPONENT_POSITION", 0, Stack.DataType.eHudComponent));
            Overrides.Add(new("REQUEST_SCALEFORM_SCRIPT_HUD_MOVIE", 0, Stack.DataType.eHudComponent));
            Overrides.Add(new("HAS_SCALEFORM_SCRIPT_HUD_MOVIE_LOADED", 0, Stack.DataType.eHudComponent));
            Overrides.Add(new("REMOVE_SCALEFORM_SCRIPT_HUD_MOVIE", 0, Stack.DataType.eHudComponent));

            // PED ped type

            Overrides.Add(new("CREATE_PED", 0, Stack.DataType.ePedType));
            Overrides.Add(new("CREATE_PED_INSIDE_VEHICLE", 1, Stack.DataType.ePedType));

            // PED ped component type

            Overrides.Add(new("GET_PED_DRAWABLE_VARIATION", 1, Stack.DataType.ePedComponentType));
            Overrides.Add(new("GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS", 1, Stack.DataType.ePedComponentType));
            Overrides.Add(new("GET_PED_TEXTURE_VARIATION", 1, Stack.DataType.ePedComponentType));
            Overrides.Add(new("GET_NUMBER_OF_PED_TEXTURE_VARIATIONS", 1, Stack.DataType.ePedComponentType));
            Overrides.Add(new("GET_PED_PALETTE_VARIATION", 1, Stack.DataType.ePedComponentType));
            Overrides.Add(new("IS_PED_COMPONENT_VARIATION_VALID", 1, Stack.DataType.ePedComponentType));
            Overrides.Add(new("SET_PED_PRELOAD_VARIATION_DATA", 1, Stack.DataType.ePedComponentType));
            Overrides.Add(new("SET_PED_PRELOAD_PROP_DATA", 1, Stack.DataType.ePedComponentType));
            Overrides.Add(new("GET_PED_PROP_INDEX", 1, Stack.DataType.ePedComponentType));
            Overrides.Add(new("GET_PED_PROP_TEXTURE_INDEX", 1, Stack.DataType.ePedComponentType));
            Overrides.Add(new("SET_PED_COMPONENT_VARIATION", 1, Stack.DataType.ePedComponentType));

            // SYSTEM, MISC stack size

            Overrides.Add(new("START_NEW_SCRIPT", 1, Stack.DataType.eStackSize));
            Overrides.Add(new("START_NEW_SCRIPT_WITH_ARGS", 3, Stack.DataType.eStackSize));
            Overrides.Add(new("START_NEW_SCRIPT_WITH_NAME_HASH", 1, Stack.DataType.eStackSize));
            Overrides.Add(new("START_NEW_SCRIPT_WITH_NAME_HASH_AND_ARGS", 3, Stack.DataType.eStackSize));
            Overrides.Add(new("GET_NUMBER_OF_FREE_STACKS_OF_THIS_SIZE", 0, Stack.DataType.eStackSize));

            // DECORATOR decorator type

            Overrides.Add(new("DECOR_REGISTER", 1, Stack.DataType.eDecoratorType));
            Overrides.Add(new("DECOR_IS_REGISTERED_AS_TYPE", 1, Stack.DataType.eDecoratorType));

            // SCRIPT event group

            Overrides.Add(new("GET_NUMBER_OF_EVENTS", 0, Stack.DataType.eEventGroup));
            Overrides.Add(new("GET_EVENT_EXISTS", 0, Stack.DataType.eEventGroup));
            Overrides.Add(new("GET_EVENT_AT_INDEX", 0, Stack.DataType.eEventGroup));
            Overrides.Add(new("GET_EVENT_DATA", 0, Stack.DataType.eEventGroup));
            Overrides.Add(new("TRIGGER_SCRIPT_EVENT", 0, Stack.DataType.eEventGroup));
            Overrides.Add(new("SEND_TU_SCRIPT_EVENT", 0, Stack.DataType.eEventGroup));

            // HUD hud colour

            Overrides.Add(new("GET_HUD_COLOUR", 0, Stack.DataType.eHudColour));
            Overrides.Add(new("REPLACE_HUD_COLOUR", 0, Stack.DataType.eHudColour));
            Overrides.Add(new("REPLACE_HUD_COLOUR", 1, Stack.DataType.eHudColour));
            Overrides.Add(new("REPLACE_HUD_COLOUR_WITH_RGBA", 0, Stack.DataType.eHudColour));
            Overrides.Add(new("SET_CUSTOM_MP_HUD_COLOR", 0, Stack.DataType.eHudColour));
            Overrides.Add(new("SET_COLOUR_OF_NEXT_TEXT_COMPONENT", 0, Stack.DataType.eHudColour));

            // HUD blip sprites

            Overrides.Add(new("GET_NEXT_BLIP_INFO_ID", 0, Stack.DataType.eBlipSprite));
            Overrides.Add(new("GET_FIRST_BLIP_INFO_ID", 0, Stack.DataType.eBlipSprite));
            Overrides.Add(new("GET_CLOSEST_BLIP_INFO_ID", 0, Stack.DataType.eBlipSprite));
            Overrides.Add(new("SET_BLIP_SPRITE", 1, Stack.DataType.eBlipSprite));
            Overrides.Add(new("GET_BLIP_SPRITE", -1, Stack.DataType.eBlipSprite));
            Overrides.Add(new("CUSTOM_MINIMAP_SET_BLIP_OBJECT", 0, Stack.DataType.eBlipSprite));

            // PED knockoff vehicle

            Overrides.Add(new("SET_PED_CAN_BE_KNOCKED_OFF_VEHICLE", 1, Stack.DataType.eKnockOffVehicle));

            // PED combat movement

            Overrides.Add(new("SET_PED_COMBAT_MOVEMENT", 1, Stack.DataType.eCombatMovement));

            // PED combat attribute

            Overrides.Add(new("SET_PED_COMBAT_ATTRIBUTES", 1, Stack.DataType.eCombatMovement));
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
