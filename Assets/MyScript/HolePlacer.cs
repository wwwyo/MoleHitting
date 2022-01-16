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
    ARPlaneManager arPlaneManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
    }

    void Update()
    {
        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }

        if (Input.touchCount > 0)
	    {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // when ray hit plane return true, and plane info in hits
                if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose pose = hits[hits.Count - 1].pose;
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
