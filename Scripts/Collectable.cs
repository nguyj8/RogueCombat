// Written by J Nguyen
// 03/19/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable 
{
    // Logic -> protected is private, but children classes have access 
    protected bool collected;

    protected override void OnCollide(Collider2D coll)
    {
        // If the name of the player is collider, then call method OnCollect
        if (coll.name == "Player")
        {
            OnCollect();
        }
    }

    protected virtual void OnCollect()
    {
        collected = true; 
    }
}
