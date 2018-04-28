using UnityEngine;
using System.Collections;

public class SceneController
{
    /// <summary>
    /// 切換到場景
    /// </summary>
    public static void ChangeScene(SceneName _name)
    {
        AsyncOperation async = Application.LoadLevelAsync(_name.ToString());
    }
}
