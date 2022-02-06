using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director: MonoBehaviour
{
    public GameObject ballPrefab;
    [SerializeField] Camera arCamera;
    Vector3 initialPos = new Vector3(0.5f,0.1f,12.5f);

    void Start()
    {
        Arrive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Arrive()
    {
        Vector3 pos = arCamera.ViewportToWorldPoint(initialPos);
        Instantiate(ballPrefab, pos, ballPrefab.transform.rotation);
    }
}
