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
        var type = typeof(ConfigData);
        var field = AccessTools.Field(type, fieldName);
        if (field != null)
        {
            accessor.Set = (config, value) => field.SetValue(config, value);
        }
        else
        {
            UnityEngine.Debug.LogError($"Field {fieldName} not found in ConfigData");
        }
    }
}