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
    bool isThrow = false;
    Vector3 startPos;
    Vector3 endPos;
    float duration;
    float xSpeed;
    float ySpeed;
    public float zSpeed = 3;
    Rigidbody m_rigidbody;
    Rect rect = new Rect(0, 0, 1, 1);

    void Start()
    {
        arCamera = Camera.main;
        Director = GameObject.Find("Director");
        initialScale = transform.localScale;
        transform.LookAt(arCamera.transform.position);
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    isThrow = true;
        //    ThrowBall();
        //}
        Debug.Log("ball position" + transform.position.ToString());
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
                    if (duration < 1 && IsFlic())
                    {
                        isThrow = true;
                        ThrowBall();
                    }
                    break;
                default:
                    break;
            }
        }
        else if (isThrow)
        {
            Vector3 new_pos = transform.position;
            m_rigidbody.AddForce(Vector3.down, ForceMode.Force); 
            if (!isVisible()) { 
                Destroy(this);
	        }
        }
        else if (!isThrow)
        {
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
        xSpeed = (endPos.x - startPos.x)/duration;
        ySpeed = (endPos.y - startPos.y)/duration;
        Debug.Log(ySpeed.ToString() + ":" + ySpeed.ToString());
        return (Mathf.Abs(ySpeed) >= 2);
    }

    void ThrowBall()
    {
        Debug.Log("throw");
        m_rigidbody.AddForce(xSpeed, ySpeed, zSpeed, ForceMode.Impulse);
        return;
    }

    bool isVisible()
    {
        var viewportPos = arCamera.WorldToViewportPoint(transform.position);
        return rect.Contains(viewportPos);
    }

}
