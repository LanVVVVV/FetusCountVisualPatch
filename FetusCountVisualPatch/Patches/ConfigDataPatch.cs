using HarmonyLib;
using MBMScripts;

namespace FetusCountVisualPatch.Patches;

[HarmonyPatch(typeof(ConfigData))]
public class ConfigDataPatch
{
    private static int MaxMultiplePregnancyCount => ModConfig.TargetMaxMultiplePregnancyCount;

    [HarmonyPatch(nameof(ConfigData.MaxMultiplePregnancyCount), MethodType.Getter)]
    [HarmonyPostfix]
    public static void MaxMultiplePregnancyCountPostfix(ref int __result)
    {
        __result = MaxMultiplePregnancyCount;
    }
}