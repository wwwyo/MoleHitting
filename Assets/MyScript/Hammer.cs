using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hammer : MonoBehaviour
{
    [SerializeField] Camera arCamera;
    int count = 0;

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
                if (hit.collider.CompareTag("Mole"))
                {
                    hit.collider.gameObject.GetComponent<MoleController>().Hit();
                    count++;
                    GameObject.Find("Score").GetComponent<Text>().text = "Score: " + count.ToString();
		        }
	        }
      //      if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
      //      {
                
		    //}
	    }
    }
}
