using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager _manager;

    public TextMeshProUGUI timeTextField;
    public TextMeshProUGUI scoreTextField;
    // Start is called before the first frame update
    void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        _manager.OnChronoUpdate += OnChronoUpdate; //Abonnement
        _manager.OnScoreUpdate += ScoreDisplay;
    }

    private void OnDestroy()
    {
        if ((_manager))
        {
            _manager.OnChronoUpdate -= OnChronoUpdate;
            _manager.OnScoreUpdate -= ScoreDisplay;
        }
    }

    private void OnChronoUpdate(TimeSpan span)
    {
        timeTextField.text= String.Format("{0:00}:{1:00}.{2:00}",
             span.Minutes, span.Seconds,
             span.Milliseconds / 10);
       // Debug.Log( "temps: " + timeTextField.text);
    }
    
    private void ScoreDisplay(int score)
    {
        scoreTextField.text = String.Format("{0}", score);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
