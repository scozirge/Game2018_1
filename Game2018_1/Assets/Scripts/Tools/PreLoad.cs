using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PreLoad : MonoBehaviour
{
    [SerializeField]
    Vector3 PreLoadPos;
    [SerializeField]
    float DestroyTime;
    [SerializeField]
    List<string> LoadFolderSprites;
    [SerializeField]
    List<GameObject> PreLoadParticles;
    [SerializeField]
    string PreloadDicTag;


    List<GameObject> GoList;
    public static Dictionary<string, bool> IsPreloadDic;

    void Awake()
    {
        if (PreloadDicTag != null && PreloadDicTag != "")
        {
            if (IsPreloadDic == null)
                IsPreloadDic = new Dictionary<string, bool>();

            if (IsPreloadDic.ContainsKey(PreloadDicTag))
                if (IsPreloadDic[PreloadDicTag])
                    return;
            PreLoadGameObject();
        }
        else
        {
            PreLoadGameObject();
        }
    }
    public void PreLoadGameObject()
    {
        GoList = new List<GameObject>();
        for (int i = 0; i < PreLoadParticles.Count; i++)
        {
            if (PreLoadParticles[i] == null)
                continue;
            GameObject go = Instantiate(PreLoadParticles[i], PreLoadPos, Quaternion.identity) as GameObject;
            GoList.Add(go);
        }
        for (int i = 0; i < LoadFolderSprites.Count; i++)
        {
            if (LoadFolderSprites[i] == "")
                continue;
            Sprite[] sprites = Resources.LoadAll<Sprite>(LoadFolderSprites[i]);
            if (sprites != null)
            {
                for (int j = 0; j < sprites.Length; j++)
                {
                    GameObject go = new GameObject();
                    go.transform.position = PreLoadPos;
                    SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                    sr.sprite = sprites[j];
                    GoList.Add(go);
                }
            }
        }
        StartCoroutine(DestroyPreloadObjs());


        if (PreloadDicTag != null && PreloadDicTag != "")
        {
            if (!IsPreloadDic.ContainsKey(PreloadDicTag))
                IsPreloadDic.Add(PreloadDicTag, true);
            else
                IsPreloadDic[PreloadDicTag] = true;
        }
    }
    IEnumerator DestroyPreloadObjs()
    {
        Debug.Log(string.Format("PreLoadPrefab:{0}", GoList.Count));
        yield return new WaitForSeconds(DestroyTime);
        for (int i = 0; i < GoList.Count; i++)
        {
            if (GoList[i] == null)
                continue;
            Destroy(GoList[i]);
        }
    }
}
