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
    public float xSpeed = 0;
    public float ySpeed = 1;
    public float zSpeed = 1;
    Rigidbody m_rigidbody;
    Rect rect = new Rect(0, 0, 1, 1);

    void Start()
    {
        arCamera = Camera.main;
        Director = GameObject.Find("Director");
        initialScale = transform.localScale;
        initialPos.x = arCamera.ScreenToViewportPoint(transform.position).x;
        initialPos.y = arCamera.ScreenToViewportPoint(transform.position).y;
        initialPos.z = transform.position.z;
        transform.LookAt(arCamera.transform.position);
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isThrow = true;
            ThrowBall();
        }
        //Debug.Log("ball position" + transform.position.ToString());
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
            m_rigidbody.AddForce(Vector3.down * 2, ForceMode.Force); 
            if (!isVisible()) { 
                Destroy(this);
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
            Director.GetComponent<Director>().Arrive(arCamera.ViewportToWorldPoint(initialPos));
	    }
    }

    bool IsFlic()
    {
        float dist = Vector3.Distance(endPos, startPos);
        xSpeed = (endPos.x - startPos.x)/duration;
        //ySpeed = (endPos.y - startPos.y)/duration;
        //Debug.Log(ySpeed.ToString() + ":" + ySpeed.ToString());
        return (Mathf.Abs(ySpeed) >= 2);
    }

    void ThrowBall()
    {
        m_rigidbody.AddForce(xSpeed, ySpeed, zSpeed, ForceMode.Impulse);
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
            Destroy(this);
	    }
    }



}
