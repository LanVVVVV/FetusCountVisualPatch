using FetusCountVisualPatch.FetusSprite;
using FetusCountVisualPatch.Properties;
using MBM.ModLoader.Core;
using UnityEngine;

namespace FetusCountVisualPatch;

public static class ModEntry
{
    internal const string ModName = "FetusCountVisualPatch";

    public const int fetus4EDataType = 34;

    public const int fetus5EDataType = 35;

    public static void Load()
    {
        UIFetusSprite.LoadSprite();

        ModConfig.MaxMultiplePregnancyCountModSetting();
        //ModConfig.Debugger();

        Localization.OnLanguageChanged += OnLanguageChanged;

        Log("FetusCountVisualPatch Mod loaded!");
    }

    private static void OnLanguageChanged(string langCode)
    {
        Strings.Culture = Localization.CurrentCulture;

        ModConfig.MaxMultiplePregnancyCountOnLanguageChanged();

        Log($"language changed: {langCode}");
    }

    internal static void Log(string msg) => Debug.Log($"[FCV] {msg}");
    internal static void LogError(string msg) => Debug.LogError($"[FCV] {msg}");
}