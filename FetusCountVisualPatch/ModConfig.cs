using FetusCountVisualPatch.Properties;
using MBM.ModLoader.Settings;

namespace FetusCountVisualPatch;

internal static class ModConfig
{
    private const string MaxMultiplePregnancyCount = "Max Multiple Pregnancy Count";

    private static readonly int[] MaxMultiplePregnancyCountValues = { 3, 4, 5};

    private static readonly string[] MaxMultiplePregnancyCountLabels = { "3", "4", "5" };

    //private const string DebuggerSwitch = "Fetus Count Debugger Switch";

    //private const string DebuggerCount = "Debugger Fetus Count";

    public static int TargetMaxMultiplePregnancyCount { get; set; }

    //public static bool BoolDebugger { get; set; }

    //public static int DebuggerFetusCount { get; set; }

    internal static void MaxMultiplePregnancyCountModSetting()
    {
        ModSettings.RegisterDropdown(ModEntry.ModName, MaxMultiplePregnancyCount, MaxMultiplePregnancyCountLabels, 0, Strings.Config_MaxMultiplePregnancyCount);
        TargetMaxMultiplePregnancyCount = MaxMultiplePregnancyCountValues[ModSettings.GetDropdown(ModEntry.ModName, MaxMultiplePregnancyCount)];
        ModSettings.OnChanged(ModEntry.ModName, MaxMultiplePregnancyCount, v =>
        {
            int max = MaxMultiplePregnancyCountValues[(int)v];
            TargetMaxMultiplePregnancyCount = max;
            ModEntry.Log($"{MaxMultiplePregnancyCount} = {TargetMaxMultiplePregnancyCount}");
        });
    }

    internal static void MaxMultiplePregnancyCountOnLanguageChanged()
    {
        ModSettings.SetDescription(ModEntry.ModName, MaxMultiplePregnancyCount, Strings.Config_MaxMultiplePregnancyCount);
    }

    //internal static void Debugger()
    //{
    //    ModSettings.RegisterBool(ModEntry.ModName, DebuggerSwitch, false, DebuggerSwitch, "Debugger");
    //    BoolDebugger = ModSettings.GetBool(ModEntry.ModName, DebuggerSwitch);
    //    ModSettings.OnChanged(ModEntry.ModName, DebuggerSwitch, v =>
    //    {
    //        if (BoolDebugger == (bool)v) return;
    //        BoolDebugger = (bool)v;
    //        ModEntry.Log($"{DebuggerSwitch} = {BoolDebugger}");
    //    });

    //    ModSettings.RegisterInt(ModEntry.ModName, DebuggerCount, 4, DebuggerCount, "Debugger", "FCVDebugger");
    //    DebuggerFetusCount = ModSettings.GetInt(ModEntry.ModName, DebuggerCount);
    //    ModSettings.OnChanged(ModEntry.ModName, DebuggerCount, v =>
    //    {
    //        if (DebuggerFetusCount == (int)v) return;
    //        DebuggerFetusCount = (int)v;
    //        ModEntry.Log($"{DebuggerCount} = {DebuggerFetusCount}");
    //    });

    //    ModSettings.SetVisibleWhen(ModEntry.ModName, DebuggerSwitch,
    //        new Dictionary<string, string[]>
    //        {
    //            { "True", new[] { "FCVDebugger" } }
    //        });
    //}
}

