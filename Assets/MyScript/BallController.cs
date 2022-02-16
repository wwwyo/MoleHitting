using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BallController : MonoBehaviour
{
    Camera arCamera;
    float initialZ = 2;
    GameObject Director;
    bool isThrow = false;
    Vector3 startPos;
    Vector3 endPos;
    float duration;
    float xSpeed;
    float ySpeed;
    float zSpeed = 1.5f;
    Rigidbody m_rigidbody;
    Rect rect = new Rect(0, 0, 1, 1);
    float throwSpan;

    void Start()
    {
        arCamera = Camera.main;
        Director = GameObject.Find("Director");
        transform.LookAt(arCamera.transform.position);
        m_rigidbody = GetComponent<Rigidbody>();
        //Debug.Log(arCamera.WorldToViewportPoint(transform.position));
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    isThrow = true;
        //    ThrowBall();
        //}
        duration += Time.deltaTime;
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = transform.position;
                    duration = 0;
                    throwSpan = 0;
                    break;
                case TouchPhase.Moved:
                    var pos = arCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, initialZ));
                    transform.position = pos;
                    break;
                case TouchPhase.Ended:
                    endPos = transform.position;
                    if (IsFlic())
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
            throwSpan += Time.deltaTime;
            m_rigidbody.AddForce(Vector3.down, ForceMode.Force); 
            if (throwSpan > 5 || !isVisible()) { 
                Destroy(this.gameObject);
	        }
        }
        else if (!isThrow)
        {
            //Vector3 pos = arCamera.ViewportToWorldPoint(initialPos);
            //transform.position = pos;
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
        xSpeed = (endPos.x - startPos.x);
        ySpeed = (endPos.y - startPos.y) * 1.5f;
        return (Mathf.Abs(ySpeed) >= 0.1);
    }

    void ThrowBall()
    {
        m_rigidbody.AddForce(xSpeed, ySpeed / 4, ySpeed, ForceMode.Impulse);
        return;
    }

    bool isVisible()
    {
        var viewportPos = arCamera.WorldToViewportPoint(transform.position);
        return rect.Contains(viewportPos);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mole")) {
            collision.gameObject.GetComponent<MoleController>().Hit();
            Destroy(this.gameObject);
	    }
    }



}
