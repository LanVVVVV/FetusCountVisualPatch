using FetusCountVisualPatch.Accessors;
using MBMScripts;

namespace FetusCountVisualPatch.Features;

public static class ConfigDataUpdater
{
    public static void ApplyAll()
    {
        UpdateMaxMultiplePregnancyCount(ModConfig.TargetMaxMultiplePregnancyCount);

        ModEntry.Log("Max Multiple Pregnancy Count Modify initialized");
    }

    public static void UpdateMaxMultiplePregnancyCount(int num)
    {
        ConfigDataAccessor.MaxMultiplePregnancyCount.Set(GameManager.ConfigData,num);
    }
}