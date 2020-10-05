using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Doozy.Engine.Soundy;
using UnityEngine;


public delegate void ChronoUpdateHandler(TimeSpan span);
public delegate void ScoreUpdateHandler(int score);


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private Stopwatch _stopwatch = new Stopwatch();
    private bool isRunning = false;
    public event ChronoUpdateHandler OnChronoUpdate;
    public event ScoreUpdateHandler OnScoreUpdate;
    
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
        
        if (OnScoreUpdate != null)
        {
           // OnScoreUpdate();
        }


    }
}
