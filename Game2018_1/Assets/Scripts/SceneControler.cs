using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
    public static void ChangeScene(string _scene)
    {
        switch (_scene)
        {
            case "Start":
                SceneManager.LoadScene(_scene);
                break;
            case "Menu":
                SceneManager.LoadScene(_scene);
                break;
            case "Battle":
                SceneManager.LoadScene(_scene);
                break;
            default:
                Debug.LogWarning(string.Format("錯誤的場景名稱:{0}", _scene));
                break;
        }
    }
}