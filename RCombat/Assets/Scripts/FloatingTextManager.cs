// Written by J Nguyen
// 03/20/22 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject); // Floating text will never be destroyed when transitioning from scene to scene 
    }

    private void Update()
    {
        // Updates every floating text every frame 
        foreach(FloatingText text in floatingTexts)
        {
            text.UpdateFloatingText(); 
        }
    }

    public void Show(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.text.text = message;
        floatingText.text.fontSize = fontSize;
        floatingText.text.color = color;
        // Transfer world space to screen space in which can be used in the UI 
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show(); 
    }


    // Floating mechanic
    private FloatingText GetFloatingText()
    {
        FloatingText text = floatingTexts.Find(t => !t.active);

        if (text == null)
        {
            text = new FloatingText();
            text.go = Instantiate(textPrefab);
            text.go.transform.SetParent(textContainer.transform);
            text.text = text.go.GetComponent<Text>();

            floatingTexts.Add(text); 
        }
        return text; 
    }
}
