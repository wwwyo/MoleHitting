using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class HolePlacer : MonoBehaviour
{
    GameObject holes;
    public GameObject holesPrefab;
    ARRaycastManager arRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
	    {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // when ray hit plane return true, and plane info in hits
                if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose pose = hits[0].pose;
                    ArriveHole(pose);
		        }
            }
        }
	   
    }

    void ArriveHole(Pose pose)
    {
        {
            if (holes)
            { 
                holes.transform.position = pose.position;
	        }
            else
            {
                holes = Instantiate(holesPrefab, pose.position, pose.rotation);
	        }
        }

    }
}
