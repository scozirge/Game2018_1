using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static bool IsInit;
    static List<AudioSource> ASList;
    static GameObject MyAudioObject;
    AudioSource CurAS;

    void Awake()
    {
        if (!IsInit)
        {
            Init();
        }
    }
    void Init()
    {
        ASList = new List<AudioSource>();
        MyAudioObject = new GameObject("AudioPlayer");
        DontDestroyOnLoad(MyAudioObject);
        MyAudioObject.AddComponent<AudioSource>();
        ASList.Add(MyAudioObject.GetComponent<AudioSource>());
        for (int i = 0; i < ASList.Count; i++)
        {
            if (!ASList[i].isPlaying)
                CurAS = ASList[i];
        }
        IsInit = true;
    }
    public void PlaySound(string _soundName)
    {
        if (IsInit)
        {
            if (GetApplicableAudioSource() != null)
            {
                CurAS.clip = Resources.Load<AudioClip>(string.Format("Sounds/{0}", _soundName));
                CurAS.Play(0);
            }
            else
            {
                GetNewAudioSource();
                CurAS.clip = Resources.Load<AudioClip>(string.Format("Sounds/{0}", _soundName));
                CurAS.Play(0);
            }
        }
        else
        {
            Init();
        }
    }
    public void PlaySound(AudioClip _ac)
    {
        if (IsInit)
        {
            if (GetApplicableAudioSource() != null)
            {
                CurAS.clip = _ac;
                CurAS.Play(0);
            }
            else
            {
                GetNewAudioSource();
                CurAS.clip = _ac;
                CurAS.Play(0);
            }
        }
        else
        {
            Init();
        }
    }
    AudioSource GetApplicableAudioSource()
    {
        CurAS = null;
        for (int i = 0; i < ASList.Count; i++)
        {
            if (!ASList[i].isPlaying)
            {
                CurAS = ASList[i];
                return CurAS;
            }
        }
        return CurAS;
    }
    AudioSource GetNewAudioSource()
    {
        CurAS = MyAudioObject.AddComponent<AudioSource>();
        ASList.Add(CurAS);
        return CurAS;
    }
}
