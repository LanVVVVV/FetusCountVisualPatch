using System.IO;
using System.Reflection;
using UnityEngine;

namespace FetusCountVisualPatch.FetusSprite;

public static class UIFetusSprite
{
    private static readonly Assembly assembly = Assembly.GetExecutingAssembly();

    public static Sprite? SpriteFetus4 { get; set; }

    public static Sprite? SpriteFetus5 { get; set; }

    public static void LoadSprite()
    {
        SpriteFetus4 = GetEmbeddedSprite("slaveui_icon_fetus_4.png");
        SpriteFetus5 = GetEmbeddedSprite("slaveui_icon_fetus_5.png");
    }

    public static Sprite? GetEmbeddedSprite(string embeddedResourceName)
    {
        using (Stream stream = assembly.GetManifestResourceStream(embeddedResourceName))
        {
            if (stream == null) 
            {
                ModEntry.LogError($"Embedded resource not found: {embeddedResourceName}");
                return null;
            }

            byte[] imageData = new byte[stream.Length];
            stream.Read(imageData, 0, imageData.Length);

            Texture2D texture = new Texture2D(1, 1);
            if (texture.LoadImage(imageData))
            {
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
            }
            else
            {
                ModEntry.LogError($"Failed to parse embedded resource: {embeddedResourceName}");
                return null;
            }
        }
    }
}
