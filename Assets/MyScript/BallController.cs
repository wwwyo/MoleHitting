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
    bool isThrow = false;
    Vector3 startPos;
    Vector3 endPos;
    float duration;
    float xSpeed;
    float ySpeed;

    void Start()
    {
        arCamera = Camera.main;
        Director = GameObject.Find("Director");
        initialScale = transform.localScale;
        transform.LookAt(arCamera.transform.position);
    }

    void Update()
    {
        duration += Time.deltaTime;
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    transform.localScale = (initialScale * 1.2f);
                    startPos = transform.position;
                    duration = 0;
                    break;
                case TouchPhase.Moved:
                    var pos = arCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 12.5f));
                    transform.position = pos;
                    break;
                case TouchPhase.Ended:
                    transform.localScale = initialScale;
                    endPos = transform.position;
                    if (duration > 0.5 && IsFlic())
                    {
                        isThrow = true;
                        ThrowBall();
                    }
                    break;
                default:
                    break;
            }
        } else {
            if (isThrow) {
                return;
	        }
            Vector3 pos = arCamera.ViewportToWorldPoint(initialPos);
            transform.position = pos;
        }
    }

    private void OnDestroy()
    {
        if (Director != null) { 
            Director.GetComponent<Director>().Arrive();
	    }
    }

    bool IsFlic()
    {
        float dist = Vector3.Distance(endPos, startPos);
        xSpeed = Mathf.Abs(endPos.x - startPos.x) / 500.0f;
        ySpeed = Mathf.Abs(endPos.y - startPos.y) / 500.0f;
        Debug.Log(xSpeed.ToString() + ":" + ySpeed.ToString());
        return (xSpeed >= 3 || ySpeed >= 3);
    }

    void ThrowBall()
    {
        return;
    }


}
