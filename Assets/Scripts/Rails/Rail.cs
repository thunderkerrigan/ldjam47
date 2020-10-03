using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public struct Coordinate
{
    public int Row { get; set; }
    public int Column { get; set; }
    
    public Coordinate(int row, int column)
    {
        Row = row;
        Column = column;
    }
}
public delegate PositionEnum RequestOpenRoutesHandler(Coordinate coordinate);
public delegate void ReceiveOpenRouteHandler(PositionEnum directions);


public class Rail : MonoBehaviour
{
    public event RequestOpenRoutesHandler OnRequestOpenRoutesHandler;
    public event ReceiveOpenRouteHandler OnReceiveOpenRouteHandler;

    public Coordinate coordinate;    
    [SerializeField] private GameObject selectionText;
    public bool isSelected = false;
    public bool isBuildable = true;
    public PositionEnum openDirection = PositionEnum.None;
    public TextMeshPro textMesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetText(string text)
    {
        textMesh.SetText(text);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectionText.activeSelf != isSelected)
        {
            
            selectionText.SetActive(isSelected);
            if (openDirection != PositionEnum.None)
            {
                isSelected = true;
                selectionText.SetActive(true);

            }
            GetOpenRoutes();
        }
    }

    public PositionEnum GetOpenRoutes()
    {
        if (OnRequestOpenRoutesHandler != null)
        {
           var openAccess = OnRequestOpenRoutesHandler(this.coordinate);
           if (OnReceiveOpenRouteHandler != null)
           {
               OnReceiveOpenRouteHandler(openAccess);
           }
        }

        return PositionEnum.None;
    } 
}
