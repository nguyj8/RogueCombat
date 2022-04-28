// Written by J Nguyen
// 03/18/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform perspective;
    public float boundX = 0.30f;
    public float boundY = 0.15f;

    private void Start()
    {
        perspective = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        // Ensures if sprite is inside the bounds on the
        // X axis 
        float deltaX = perspective.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < perspective.position.x)
            {
                delta.x = deltaX - boundX; 
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }
        // Y axis 
        float deltaY = perspective.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < perspective.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);


    }
}
