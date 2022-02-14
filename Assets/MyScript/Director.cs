using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director: MonoBehaviour
{
    public GameObject ballPrefab;
    [SerializeField] Camera arCamera;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Arrive(Vector3 pos)
    { 
        Instantiate(ballPrefab, pos, ballPrefab.transform.rotation);
    }
}
