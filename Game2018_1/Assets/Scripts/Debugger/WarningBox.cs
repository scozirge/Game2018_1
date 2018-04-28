using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WarningBox : MonoBehaviour
{
    static Text Text_Title;
    static Text Text_Log;
    public void Init()
    {
        //Text_Title=transform.FindChild("title").GetComponent<>
    }
    public static void ShowLog(string _title, string _log)
    {
       // Text_Title.text = _title;
        //Text_Log.text = _log;
        //gameObject.SetActive(true);
    }
    public static void HideLog()
    {
        //gameObject.SetActive(false);
    }
}
