using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class HolePlacer : MonoBehaviour
{
    GameObject holes;
    public GameObject holesPrefab;
    public GameObject molePrefab;
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
        //foreach (var plane in arPlaneManager.trackables)
        //{
        //    //plane.gameObject.SetActive(false);
        //}

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
            if (!holes)
            { 
                holes = Instantiate(holesPrefab, pose.position, pose.rotation);
                GenerateMoles();
	        }
            else
            {
                //holes.transform.position = pose.position;
	        }
        }
    }

    void GenerateMoles()
    { 
        foreach (Transform t in holes.transform)
        {
            GameObject hole = t.gameObject;
            if (hole.tag == "Hole")
            {
                Vector3 pos = hole.transform.position;
                Instantiate(molePrefab, pos, molePrefab.transform.rotation);
	        }
	    }
    }
}
