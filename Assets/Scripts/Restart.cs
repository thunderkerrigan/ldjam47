using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Doozy.Engine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    
    public void OnRestartButtonClick()
    {
        var _manager = GameObject.FindGameObjectWithTag("GameController");
        if (_manager)
        {
            _manager.GetComponent<GameManager>().StartGame();
        }

    }
}
