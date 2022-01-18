using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Hammer : MonoBehaviour
{
    [SerializeField] Camera arCamera;
    //ARRaycastManager arRaycastManager;
    //List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        //arRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }

        var touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            RaycastHit hit;
            var ray = arCamera.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray,out hit))
            { 
                if (hit.collider.CompareTag("Hole"))
                {
                    hit.collider.gameObject.GetComponent<MoleController>().Hit();
		        }
	        }
      //      if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
      //      {
                
		    //}
	    }
    }
}
