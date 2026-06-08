using HarmonyLib;
using MBMScripts;
using System;

namespace FetusCountVisualPatch.Accessors;

public static class ConfigDataAccessor
{
    public class Setter<T>
    {
        public Action<ConfigData, T> Set = null!;
    }

    public static readonly Setter<int> MaxMultiplePregnancyCount = new();

    static ConfigDataAccessor()
    {
        BindField(MaxMultiplePregnancyCount, "m_MaxMultiplePregnancyCount");
    }

    private static void BindField<T>(Setter<T> accessor, string fieldName)
    {
        var fieldRef = AccessTools.FieldRefAccess<ConfigData, T>(fieldName);

        if (fieldRef == null)
        {
            ModEntry.LogError($"FieldRef for {fieldName} is null");
            return;
        }

        accessor.Set = (config, value) => fieldRef(config) = value;
    }
}