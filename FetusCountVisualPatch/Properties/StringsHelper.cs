namespace FetusCountVisualPatch.Properties;

internal static class StringsHelper
{
    public static string Get(string key)
    {
        return Strings.ResourceManager.GetString(key, Strings.Culture) ?? key;
    }
}
