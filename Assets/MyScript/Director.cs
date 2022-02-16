using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director: MonoBehaviour
{
    public GameObject ballPrefab;
    [SerializeField] Camera arCamera;

    void Start()
    {
        GameObject ball = GameObject.Find("MonsterBallPrefab");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Arrive()
    {
        Vector3 pos = arCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, 2));
        Instantiate(ballPrefab, pos, ballPrefab.transform.rotation);
    }
}
