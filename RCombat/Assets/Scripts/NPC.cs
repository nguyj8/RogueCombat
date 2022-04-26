using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Collidable 
{
    public string message;

    private float cooldown = 3.5f;
    private float call;

    protected override void Start()
    {
        base.Start(); 
        call = -cooldown; 
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - call > cooldown)
        {
            call = Time.time; 
            GameManager.instance.ShowText(message, 25, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
        }
    }
}
