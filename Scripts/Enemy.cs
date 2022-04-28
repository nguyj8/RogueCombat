// Written by J Nguyen
// 03/20/22 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // Experience
    public int xpValue = 1; // Can be changed directly in inspector

    // Logic - 
    public float triggerLength = 0.3f; // Distance <= 0.3
    public float chaseLength = 1; // Chase player for 1 meter
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hit box
    public ContactFilter2D filter;
    private BoxCollider2D hitBox;
    // Hits array 
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        // Start from enemy, then from children where there is a box collider
        hitBox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // Player in range?

        // Check player is in starting position and chase length
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
            {
                chasing = true; 
            }
            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }

        // Reset chasing 
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        // Check for overlaps
        collidingWithPlayer = false;
        // Collision work:
        // Take boxCollider and seeks other box colliders and insert into hits array
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }
            // Clean array
            hits[i] = null;
        }
    }

    // Removes enemy after dying 
    protected override void Death()
    {
        Destroy(gameObject);
        // Give player XP 
        GameManager.instance.GrantXP(xpValue);
        // Show XP 
        GameManager.instance.ShowText("+" + xpValue + " XP", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }
}