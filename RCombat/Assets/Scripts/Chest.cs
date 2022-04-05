// Written by J Nguyen
// 03/19/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable // Chest inherits Collectable
{
    public Sprite emptyChest;
    public int treasureAmount = 5; 

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.treasure += treasureAmount; 
            GameManager.instance.ShowText("+" + treasureAmount + " Gold", 25, Color.yellow, transform.position, Vector3.up * 50, 1.2f);
        }
        
    }
}
