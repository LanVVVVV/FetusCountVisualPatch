using HarmonyLib;
using MBMScripts;

namespace FetusCountVisualPatch.Patches;

[HarmonyPatch(typeof(ReferenceCharacterStatePlate))]
public static class ReferenceCharacterStatePlatePatch
{
    public const int EDataType_Fetus3 = 7;

    public const int EDataType_Fetus4 = 34;

    public const int EDataType_Fetus5 = 35;

    [HarmonyPatch(nameof(ReferenceCharacterStatePlate.Initialize))]
    [HarmonyPostfix]
    public static void InitializePostfix(ReferenceCharacterStatePlate __instance, int ___m_DataType)
    {
        if (___m_DataType != EDataType_Fetus4 && ___m_DataType != EDataType_Fetus5) return;
        __instance.ReferenceType = EReferenceType.Bool;
    }

    [HarmonyPatch(nameof(ReferenceCharacterStatePlate.GetBool))]
    [HarmonyPriority(Priority.Last)]
    [HarmonyPostfix]
    public static void GetBoolPostfix(ReferenceCharacterStatePlate __instance, int ___m_DataType, ref bool __result)
    {
        if (___m_DataType != EDataType_Fetus3 && ___m_DataType != EDataType_Fetus4 && ___m_DataType != EDataType_Fetus5) return;

        TargetUnit? targetUnit = __instance.Updater.TargetUnit;
        Character? character = (targetUnit != null) ? targetUnit.Unit as Character : null;
        if (character == null) return;

        switch (___m_DataType)
        {
            case EDataType_Fetus3:
                __result = character.FetusCount == 3;
                return;
            case EDataType_Fetus4:
                __result = character.FetusCount == 4;
                return;
            case EDataType_Fetus5:
                __result = character.FetusCount >= 5;
                return;
        }
    }
}
