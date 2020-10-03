using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Coordinate
{
    public int Row;
    public int Column;

    public Coordinate(int row, int column)
    {
        Row = row;
        Column = column;
    }
}

public class Rail : MonoBehaviour
{

    public Coordinate position;
    [SerializeField] private GameObject selectionText;
    public bool isSelected = false;
    public bool isBuildable = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectionText.activeSelf != isSelected)
        {
            selectionText.SetActive(isSelected);
        }
    }
}
