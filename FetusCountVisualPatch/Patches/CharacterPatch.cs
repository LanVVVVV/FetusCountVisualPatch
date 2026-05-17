using HarmonyLib;
using MBMScripts;

namespace FetusCountVisualPatch.Patches;

[HarmonyPatch(typeof(Character))]
public class CharacterPatch
{
    [HarmonyPatch(nameof(Character.FetusCount), MethodType.Getter)]
    [HarmonyPostfix]
    public static void FetusCountPostfix(ref int __result)
    {
        if (__result == 0) return;
        if (ModConfig.BoolDebugger) __result = ModConfig.DebuggerFetusCount;
    }
}