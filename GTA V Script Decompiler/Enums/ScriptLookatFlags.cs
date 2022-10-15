namespace Decompiler.Enums
{
    internal enum ScriptLookatFlags
    {
        SLF_SLOW_TURN_RATE = 1,
        SLF_FAST_TURN_RATE = 2,
        SLF_EXTEND_YAW_LIMIT = 4,
        SLF_EXTEND_PITCH_LIMIT = 8,
        SLF_WIDEST_YAW_LIMIT = 16,
        SLF_WIDEST_PITCH_LIMIT = 32,
        SLF_NARROW_YAW_LIMIT = 64,
        SLF_NARROW_PITCH_LIMIT = 128,
        SLF_NARROWEST_YAW_LIMIT = 256,
        SLF_NARROWEST_PITCH_LIMIT = 512,
        SLF_USE_TORSO = 1024,
        SLF_WHILE_NOT_IN_FOV = 2048,
        SLF_USE_CAMERA_FOCUS = 4096,
        SLF_USE_EYES_ONLY = 8192,
        SLF_USE_LOOK_DIR = 16384,
        SLF_FROM_SCRIPT = 32768,
        SLF_USE_REF_DIR_ABSOLUTE = 65536
    }
}
