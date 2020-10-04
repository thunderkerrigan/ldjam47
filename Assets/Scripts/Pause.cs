using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsPaused)
        {
            Resume();
        }else
        {
            PauseStop();
        }
    }

    public void Resume()
    {
       Time.timeScale = 1f;
       GameIsPaused = false;
    }

    public void PauseStop()
    {
       Time.timeScale = 0f;
       GameIsPaused = true;
    }
}
