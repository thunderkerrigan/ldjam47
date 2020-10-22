using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine;
using Doozy.Engine.Soundy;
using Doozy.Engine.Utils.ColorModels;
using UnityEngine;

public delegate void OnScollHandler(int offset);
public delegate void OnClickHandler();

public class MouseController : MonoBehaviour
{
    
    public event OnScollHandler OnScroll;
    public event OnClickHandler OnClick;
    void Update()
    {
        
        if (OnClick != null && (Input.GetMouseButtonDown(0) | Input.GetMouseButtonDown(1)))
        {
            OnClick();
        }
        
        if (OnScroll != null && Input.mouseScrollDelta.y != 0)
        {
            OnScroll(Mathf.RoundToInt(Input.mouseScrollDelta.y));
        }
        if (OnScroll != null && Input.GetButtonDown("Jump"))
        {
            OnScroll(1);
        }
    }
}
