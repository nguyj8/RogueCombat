// Written by J Nguyen
// 03/18/22 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ReceiveDamage(Damage damage)
    {
        if (!isAlive)
        {
            return; 
        }

        base.ReceiveDamage(damage);
        GameManager.instance.OnHealthChange();
    }
    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.death.SetTrigger("Show");
        //GameManager.instance.Respawn();x 
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (isAlive)
        {
            UpdateMotor(new Vector3(x, y, 0));
        }
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }
    public void LevelUp()
    {
        //maxHitPoint++;
        //hitPoint = maxHitPoint;
        GameManager.instance.ShowText("+" + " 1 UP", 25, Color.green, transform.position, Vector3.up * 50, 1.2f);
    }
    public void SetLevel(int level) // When starting game, may set the level 
    {
        for (int i = 0; i < level; i++)
        {
            LevelUp();
        }
    }
    public void Respawn() // Game Over
    {
        isAlive = true;
        maxHitPoint = 3;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }

}