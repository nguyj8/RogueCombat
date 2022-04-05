// Written by J Nguyen
// 03/20/22 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cannot drag and drop from object but must be inherited 
public abstract class Mover : Fighter // Anything that moves is treated as a fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;

    protected virtual void Start() // may be overwritten
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Differentiate Player from NPC
    protected virtual void UpdateMotor(Vector3 input)
    {
        // Reset moveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        // Swap sprite direction
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Add push vector, if any
        moveDelta += pushDirection;

        // Reduce push force per frame based off recovery speed of both enemy and player
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed); 


        // Ensure sprite move correctly
        // Y-axis
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Move sprite
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0); 
        }
        // X-axis 
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Move sprite
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

    }

}
