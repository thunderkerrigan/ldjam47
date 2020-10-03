using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public enum PositionEnum
{
    Left,
    Right,
    Top,
    Down
}
public class GridManager : MonoBehaviour
{

    public Rail EmptyRailItem;
    private List<List<Rail>> gridItems;
    
    // Start is called before the first frame update
    void Start()
    {
        gridItems = new List<List<Rail>>(10);
        InitGrid();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */

    public void InitGrid()
    {
        ClearGrid();
        Vector3 currentPosition = new Vector3();
        for (int i = 0; i < 10; i++)
        {
            var newRow = new List<Rail>();
            for (int j = 0; j < 10; j++)
            {
                var newRail = Instantiate(EmptyRailItem, currentPosition, Quaternion.identity);
                newRail.position = new Coordinate(i,j);
                newRow.Add(newRail);
                currentPosition += Vector3.right;
            }
            gridItems.Add(newRow);
            currentPosition.x = 0;
            currentPosition += Vector3.back;
        }
    }

    public Rail NextRail(Rail requestingRail, PositionEnum position)
    {
        var nextRailCoordinate = new Coordinate(requestingRail.position.Row, requestingRail.position.Column);
        switch (position)
        {
            case PositionEnum.Left:
                nextRailCoordinate.Row -= 1;
                break;
            case PositionEnum.Right:
                nextRailCoordinate.Row += 1;
                break;
            case PositionEnum.Top:
                nextRailCoordinate.Column -= 1;
                break;
            case PositionEnum.Down:
                nextRailCoordinate.Column += 1;
                break;
        }

        if (nextRailCoordinate.Row >= 0 && nextRailCoordinate.Column <= 10 && nextRailCoordinate.Column >= 0 && nextRailCoordinate.Row <= 10)
        {
            return  gridItems[nextRailCoordinate.Row][nextRailCoordinate.Column];
        }
        return null;
    }
    
    public void ConstructRail(Rail requestedRail, Coordinate position)
    {

        var targetRail = gridItems[position.Row][position.Column];
        if (targetRail.isBuildable)
        {
            var gridPosition = targetRail.gameObject.transform.position;
            Destroy(targetRail);
            var newRail = Instantiate(requestedRail, gridPosition, Quaternion.identity);
            gridItems[position.Row][position.Column] = newRail;
        }
    }

    private void ClearGrid()
    {
        foreach (var row in gridItems)
        {
            foreach (var item in row)
            {
                Destroy(item);
            }
        }
        gridItems = new List<List<Rail>>(10);
    }
    
}
