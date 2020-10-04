﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager _manager;

    public Text timeTextField;
    // Start is called before the first frame update
    void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        _manager.OnChronoUpdate += OnChronoUpdate;
    }

    private void OnChronoUpdate(TimeSpan span)
    {
        timeTextField.text = String.Format("{0:00}:{1:00}.{2:00}",
             span.Minutes, span.Seconds,
             span.Milliseconds / 10);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
