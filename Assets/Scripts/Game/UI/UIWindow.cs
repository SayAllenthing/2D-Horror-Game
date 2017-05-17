using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    CanvasGroup canvas;
    
	// Use this for initialization
	void Start ()
    {
        canvas = GetComponent<CanvasGroup>();
	}
	
    public virtual void Show()
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    public virtual void Hide()
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }
}
