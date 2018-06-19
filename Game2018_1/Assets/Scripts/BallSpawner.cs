using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    Transform Trans_SpawnPos;
    [SerializeField]
    BallPrefab MyBallPrefab;
    [SerializeField]
    Camera MyCamera;
    [SerializeField]
    GameObject StartPosPrefab;
    [SerializeField]
    GameObject EndPosPrefab;

    Vector3 startPos;
    Vector3 endPos;
    GameObject Go_StartPos;
    GameObject Go_EndPos;
    public void Start()
    {
        Go_EndPos = Instantiate(StartPosPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        Go_StartPos = Instantiate(EndPosPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        Go_StartPos.SetActive(false);
        Go_EndPos.SetActive(false);
    }
    public void Update()
    {
        ClickToSpawn();
    }
    void ClickToSpawn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MyCamera.ScreenPointToRay(Input.mousePosition);
            startPos = ray.origin + (ray.direction * MyCamera.transform.position.z * -1);

            Go_StartPos.SetActive(true);
            Go_EndPos.SetActive(true);
            Go_EndPos.transform.position = startPos;
            Go_StartPos.transform.position = startPos;

        }
        if(Input.GetMouseButton(0))
        {
            Ray ray = MyCamera.ScreenPointToRay(Input.mousePosition);
            Go_EndPos.transform.position = ray.origin + (ray.direction * MyCamera.transform.position.z * -1);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Go_StartPos.SetActive(false);
            Go_EndPos.SetActive(false);
            Ray ray = MyCamera.ScreenPointToRay(Input.mousePosition);
            endPos = ray.origin + (ray.direction * MyCamera.transform.position.z * -1);
            Vector3 force = (startPos - endPos) * 250;
            Spawn(force);
        }
    }
    public void Spawn(Vector2 _force)
    {
        GameObject ballGo = Instantiate(MyBallPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        BallPrefab bp = ballGo.GetComponent<BallPrefab>();
        bp.transform.SetParent(transform);
        bp.transform.localPosition = Trans_SpawnPos.localPosition;
        bp.Init(_force);
    }
}
