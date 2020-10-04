using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public delegate void ChronoUpdateHandler(TimeSpan span);
//public Trains int;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private Stopwatch _stopwatch = new Stopwatch();
    private bool isRunning = false;
    public event ChronoUpdateHandler OnChronoUpdate;

    void Start()
    {
        
    }


    void StartGame()
    {
        isRunning = true;
        _stopwatch.Start();
    }

    private void FixedUpdate()
    {
        if (OnChronoUpdate != null)
        {
            OnChronoUpdate(_stopwatch.Elapsed);
        }
    }
}
