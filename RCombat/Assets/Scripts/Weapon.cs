// Written by J Nguyen
// 03/20/22 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage structure
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7, 8};
    public float[] pushForce = { 2.0f, 2.25f, 2.5f, 2.75f, 3.0f, 3.25f, 3.5f, 4.0f }; // Pushes away enemy when hit 


    // Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer; // Change weapon sprite

    // Swing weapon
    private Animator anim;
    private float coolDown = 0.5f;
    private float lastSwing; // Used w/ cooldown to ensure swing

    protected override void Start()
    {
        base.Start();
        //spriteRenderer = GetComponent<SpriteRenderer>(); Place in Inspector 
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)) // Space bar to swing weapon 
        {
            if (Time.time - lastSwing > coolDown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
            {
                return; 
            }

            // Damage structure
            // Create damage object, then send to fighter hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", dmg); 
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        // Increase weapon level 
        weaponLevel++;
        // Change weapon sprite 
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level; 
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
