using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : Collidable 
{
    // Damage
    public int damage = 1;
    public float pushForce = 5;

    // Once collide...
    protected override void OnCollide(Collider2D coll)
    {
        // Hit player condition
        if (coll.tag == "Fighter" && coll.name == "Player")
        {
            // Create new damage object before sending to player
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
