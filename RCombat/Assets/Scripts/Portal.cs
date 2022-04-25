// Written by J Nguyen
// 03/19/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable 
{
    public string[] sceneNames; 

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            // Teleport the player
            GameManager.instance.SaveState();
            // May alter in Unity Inspector 
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)]; // Total of 4 scenes 
            SceneManager.LoadScene(sceneName);
        }
    }
}
