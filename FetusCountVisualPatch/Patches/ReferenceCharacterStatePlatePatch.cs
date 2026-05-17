using HarmonyLib;
using MBMScripts;

namespace FetusCountVisualPatch.Patches;

[HarmonyPatch(typeof(ReferenceCharacterStatePlate))]
public static class ReferenceCharacterStatePlatePatch
{
    [HarmonyPatch(nameof(ReferenceCharacterStatePlate.Initialize))]
    [HarmonyPostfix]
    public static void InitializePostfix(ReferenceCharacterStatePlate __instance, int ___m_DataType)
    {
        if (___m_DataType != ModEntry.fetus4EDataType && ___m_DataType != ModEntry.fetus5EDataType) return;
        __instance.ReferenceType = EReferenceType.Bool;
    }

    [HarmonyPatch(nameof(ReferenceCharacterStatePlate.GetBool))]
    [HarmonyPostfix]
    public static void GetBoolPostfix(ReferenceCharacterStatePlate __instance, int ___m_DataType, ref bool __result)
    {
        if (___m_DataType != 7 && ___m_DataType != ModEntry.fetus4EDataType && ___m_DataType != ModEntry.fetus5EDataType) return;

        TargetUnit? targetUnit = __instance.Updater.TargetUnit;
        Character? character = (targetUnit != null) ? targetUnit.Unit as Character : null;
        if (character == null) return;

        switch (___m_DataType)
        {
            case 7:
                __result = character.FetusCount == 3;
                return;
            case ModEntry.fetus4EDataType:
                __result = character.FetusCount == 4;
                return;
            case ModEntry.fetus5EDataType:
                __result = character.FetusCount >= 5;
                return;
        }
    }
}
