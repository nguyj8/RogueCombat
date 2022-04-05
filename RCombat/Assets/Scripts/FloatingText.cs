// Written by J Nguyen
// 03/20/22 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject go;
    public Text text;
    public Vector3 motion;
    public float duration; 
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }
    public void Hide()
    {
        active = false;
        go.SetActive(active); 
    }

    public void UpdateFloatingText()
    {
        if (!active)
        {
            return;
        }
        // Current time - showing text is greater than duratiion, then hide text
        if (Time.time - lastShown > duration)
        {
            Hide();

        }
        go.transform.position += motion * Time.deltaTime; 
    }
}
