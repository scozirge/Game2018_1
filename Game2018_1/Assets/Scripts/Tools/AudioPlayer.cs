using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static bool IsInit;
    static List<AudioSource> ASList;
    static GameObject MyAudioObject;
    static Dictionary<string, AudioSource> LoopAudioDic;
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
        LoopAudioDic = new Dictionary<string, AudioSource>();
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
                CurAS.loop = false;
                CurAS.clip = Resources.Load<AudioClip>(string.Format("Sounds/{0}", _soundName));
                CurAS.Play(0);
            }
            else
            {
                GetNewAudioSource();
                CurAS.loop = false;
                CurAS.clip = Resources.Load<AudioClip>(string.Format("Sounds/{0}", _soundName));
                CurAS.Play(0);
            }
        }
        else
        {
            Init();
            CurAS.loop = false;
            CurAS.Play(0);
        }
    }
    public void PlaySound(AudioClip _ac)
    {
        if (IsInit)
        {
            if (GetApplicableAudioSource() != null)
            {
                CurAS.clip = _ac;
                CurAS.loop = false;
                CurAS.Play(0);
            }
            else
            {
                GetNewAudioSource();
                CurAS.loop = false;
                CurAS.clip = _ac;
                CurAS.Play(0);
            }
        }
        else
        {
            Init();
            CurAS.loop = false;
            CurAS.Play(0);
        }
    }
    public void PlayLoopSound(AudioClip _ac,string _key,bool _play)
    {
        if(_play)
        {
            if (LoopAudioDic.ContainsKey(_key))
            {
                Debug.LogWarning(string.Format("Key:{0} 循環播放音效索引重複", _key));
                return;
            }
            if (IsInit)
            {
                if (GetApplicableAudioSource() != null)
                {
                    CurAS.loop = true;
                    CurAS.clip = _ac;
                    CurAS.Play();
                }
                else
                {
                    GetNewAudioSource();
                    CurAS.loop = true;
                    CurAS.clip = _ac;
                    CurAS.Play();
                }
            }
            else
            {
                Init();
                CurAS.loop = true;
                CurAS.Play();
            }
            LoopAudioDic.Add(_key, CurAS);
        }
        else
        {
            if (LoopAudioDic.ContainsKey(_key))
            {
                LoopAudioDic[_key].Stop();
                LoopAudioDic[_key].loop = false;
                LoopAudioDic.Remove(_key);
            }
            else
                Debug.LogWarning(string.Format("Key:{0}　不存在尋換播放音效清單中", _key));
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
