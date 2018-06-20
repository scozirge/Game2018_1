using UnityEngine;
using System.Collections;

public partial class Player
{
    public static Language UseLanguage { get; private set; }
    /// <summary>
    /// 設定語言
    /// </summary>
    public static void SetLanguage(Language _language)
    {
        UseLanguage = _language;
    }
}
