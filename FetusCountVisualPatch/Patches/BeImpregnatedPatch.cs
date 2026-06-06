using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace FetusCountVisualPatch.Patches;

[HarmonyPatch]
public class BeImpregnatedPatch
{
    [HarmonyTargetMethod]
    public static MethodBase TargetMethod()
    {
        var type = typeof(MBMScripts.Character);
        var allMethods = type.GetMethods(
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);

        foreach(var method in allMethods)
        {
            if(method.Name.Contains("GetMultiplePregnancyCount") && method.Name.Contains("BeImpregnated"))
            {
                return method;
            }
        }

        throw new System.Exception("Could not find the nested GetMultiplePregnancyCount method");
    }

    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);

        for(int i = 0; i < codes.Count; i++)
        {
            if(codes[i].opcode == OpCodes.Ldc_I4_3 &&
                codes[i + 1].opcode == OpCodes.Call &&
                codes[i + 1].operand is MethodInfo method &&
                method.Name == "Clamp" &&
                method.DeclaringType?.Name == "Mathf")
            {
                codes[i] = new CodeInstruction(OpCodes.Ldc_I4_5);
                break;
            }
        }

        return codes;
    }
}