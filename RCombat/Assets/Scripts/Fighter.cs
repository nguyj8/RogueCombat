// Written by J Nguyen
// 03/20/22 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitPoint = 3;
    public int maxHitPoint = 3;
    public float pushRecoverySpeed = 0.2f;

    // Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    // Push
    protected Vector3 pushDirection;

    // All fighters can receive damage and die
    protected virtual void ReceiveDamage(Damage damage)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPoint -= damage.damageAmount;
            pushDirection = (transform.position - damage.origin).normalized * damage.pushForce; // Push to position

            // Visual to taken damage
            GameManager.instance.ShowText(damage.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);

            // Check
            if (hitPoint <= 0)
            {
                hitPoint = 0;
                Death(); 
            }
        }
    }

    protected virtual void Death()
    {
        //GameManager.instance.player.Respawn();
    }
}
