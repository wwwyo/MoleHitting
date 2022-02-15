using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director: MonoBehaviour
{
    public GameObject ballPrefab;
    [SerializeField] Camera arCamera;
    Vector3 initialPos;

    void Start()
    {
        GameObject ball = GameObject.Find("MonsterBallPrefab");
        initialPos.x = arCamera.WorldToViewportPoint(ball.transform.position).x;
        initialPos.y = arCamera.WorldToViewportPoint(ball.transform.position).y;
        initialPos.z = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Arrive()
    {
        Instantiate(ballPrefab, arCamera.ViewportToWorldPoint(initialPos), ballPrefab.transform.rotation);
    }
}
