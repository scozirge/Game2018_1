using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOManager : MonoBehaviour
{

    public static void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
    {
        byte[] _bytes = _texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(_fullPath, _bytes);
        Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + _fullPath);
    }
    public static Sprite LoadPNGAsSprite(string _fullPath)
    {
        byte[] bytes = System.IO.File.ReadAllBytes(_fullPath);
        Texture2D t = new Texture2D(1, 1);
        t.LoadImage(bytes);
        return Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
    }
    public static Texture2D LoadPNGAsTexture(string _fullPath)
    {
        byte[] bytes = System.IO.File.ReadAllBytes(_fullPath);
        Texture2D t = new Texture2D(1, 1);
        t.LoadImage(bytes);
        return t;
    }
    public static Sprite ChangeTextureToSprite(Texture2D _texture)
    {
        Sprite s = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.5f, 0.5f));
        return s;
    }
}
