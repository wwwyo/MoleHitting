using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BallController : MonoBehaviour
{
    Camera arCamera;
    Vector3 initialPos = new Vector3(0.5f, 0.1f, 12.5f);
    Vector3 initialScale;
    GameObject Director;
    ARRaycastManager arRaycastManager;
    ARPlaneManager arPlaneManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        arCamera = Camera.main;
        Director = GameObject.Find("Director");
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    transform.localScale = (initialScale * 1.2f);
                    break;
                case TouchPhase.Moved:
                    var pos = arCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 12.5f));
                    transform.position = pos;
                    break;
                case TouchPhase.Ended:
                    transform.localScale = initialScale;
                    break;
                default:
                    break;
            }
        } else {
            Vector3 pos = arCamera.ViewportToWorldPoint(initialPos);
            transform.position = pos;
            //transform.LookAt(arCamera.transform);
        }
    }

    private void OnDestroy()
    {
        if (Director != null) { 
            Director.GetComponent<Director>().Arrive();
	    }
    }

}
