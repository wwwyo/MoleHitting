using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    Vector3 groundLevel;
    Vector3 undergroundLevel;
    float time = 0;
    public float timeSpan = 2.0f;
    public GameObject effect;

    bool isOnGround = true;

    private void Up()
    {
        transform.position = groundLevel;
        isOnGround = true;
    }

    private void Down()
    {
        transform.position = undergroundLevel;
        isOnGround = false; 
    }

    // Start is called before the first frame update
    void Start()
    {
        groundLevel = transform.position;
        undergroundLevel = groundLevel - new Vector3(0,0.2f,0);

        Down();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > timeSpan)
        {
            this.time = 0;
            if (isOnGround)
            {
                Down();
	        }
            else
            {
                Up();
	        }
	    }
    }
}
