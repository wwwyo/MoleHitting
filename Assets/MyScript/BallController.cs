using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BallController : MonoBehaviour
{
    public GameObject ballPrefab;
    [SerializeField] Camera arCamera;

    void Start()
    {
        Arrive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Arrive()
    {
        Vector3 pos = arCamera.transform.position;
        pos.y += -5.67f;
        pos.z += 12.5f;
        Instantiate(ballPrefab, pos, ballPrefab.transform.rotation);
    }

}
