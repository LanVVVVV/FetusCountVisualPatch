using FetusCountVisualPatch.Features;
using FetusCountVisualPatch.Properties;
using MBM.ModLoader.Settings;
using System;

namespace FetusCountVisualPatch;

internal static class ModConfig
{
    private const string MaxMultiplePregnancyCount = "Max Multiple Pregnancy Count";

    private static readonly int[] MaxMultiplePregnancyCountValues = [3, 4, 5];

    private static readonly string[] MaxMultiplePregnancyCountLabels = ["3", "4", "5"];

    private static event Action<int>? OnMaxMultiplePregnancyCountModSettingChange;

    public static int TargetMaxMultiplePregnancyCount { get; set; }

    public static void RegisterEvents()
    {
        ModConfig.OnMaxMultiplePregnancyCountModSettingChange += ConfigDataUpdater.UpdateMaxMultiplePregnancyCount;

    }

    public static void MaxMultiplePregnancyCountModSetting()
    {
        ModSettings.RegisterDropdown(ModEntry.ModName, MaxMultiplePregnancyCount, MaxMultiplePregnancyCountLabels, 0, Strings.Config_MaxMultiplePregnancyCount);
        TargetMaxMultiplePregnancyCount = MaxMultiplePregnancyCountValues[ModSettings.GetDropdown(ModEntry.ModName, MaxMultiplePregnancyCount)];
        ModSettings.OnChanged(ModEntry.ModName, MaxMultiplePregnancyCount, v =>
        {
            int max = MaxMultiplePregnancyCountValues[(int)v];
            TargetMaxMultiplePregnancyCount = max;
            OnMaxMultiplePregnancyCountModSettingChange?.Invoke(max);
            ModEntry.Log($"{MaxMultiplePregnancyCount} = {TargetMaxMultiplePregnancyCount}");
        });
    }

    public static void MaxMultiplePregnancyCountOnLanguageChanged()
    {
        ModSettings.SetDescription(ModEntry.ModName, MaxMultiplePregnancyCount, Strings.Config_MaxMultiplePregnancyCount);
    }
}

