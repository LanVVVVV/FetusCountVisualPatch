using FetusCountVisualPatch.Features;
using FetusCountVisualPatch.Patches;
using FetusCountVisualPatch.Properties;
using FetusCountVisualPatch.Sprites;
using MBM.ModLoader.Core;
using UnityEngine;

namespace FetusCountVisualPatch;

public static class ModEntry
{
    internal const string ModName = "FetusCountVisualPatch";

    public static void Load()
    {
        FetusSprite.LoadSprite();

        ModConfig.MaxMultiplePregnancyCountModSetting();
        //ModConfig.Debugger();

        FetusCountVisual.Inject();

        ModConfig.RegisterEvents();
        ConfigDataUpdater.ApplyAll();

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