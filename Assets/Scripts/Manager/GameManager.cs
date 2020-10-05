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
    private SpawnManager _smanager;
    private GridManager _gridManager;

    private int TotalSpawn;
    private Stopwatch _stopwatch = new Stopwatch();
    private bool isRunning = false;
    public event ChronoUpdateHandler OnChronoUpdate;
    public event ScoreUpdateHandler OnScoreUpdate;

    // public int Score;
    void Start()
    {

        _gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
        SoundyManager.Play("Game", "Music 1");
        _stopwatch.Reset();
        
        _stopwatch.Start();
    }


    public void StartGame()
    {
        isRunning = true;
        _stopwatch.Reset();
        _stopwatch.Start();
        _gridManager.InitGrid();
    }

    void StopGame( GameObject train)
    {
        if (_smanager != null)
        {
            _stopwatch.Stop();
            _smanager.StopSpawnList();
        }
    }

    private void FixedUpdate()
    {
        if (OnChronoUpdate != null)
        {
            OnChronoUpdate(_stopwatch.Elapsed);
            
        } 
        
        if (OnScoreUpdate != null)
        {
            OnScoreUpdate(TotalSpawn);
        }
    }
    
    public void hookspawnmanager(SpawnManager manager)
    {
        _smanager = manager;
        manager.OnSpawnUpdate += SpawnTotal;
        manager.OnDeathUpdate += StopGame;
    }
    public void SpawnTotal(int spawn)
    {
        TotalSpawn = spawn;
    }
}
