using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[Flags]
public enum PositionEnum
{
    None = 0,
    Left = 1,
    Right=2,
    Top=4,
    Down=8
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
                newRail.coordinate = new Coordinate(i,j);
                newRail.OnRequestOpenRoutesHandler += FetchNeighborRails;
                newRail.SetText("row:"+i + " ; col: " + j);
                newRow.Add(newRail);
                if (i == 5 && j == 6)
                {
                    newRail.openDirection = PositionEnum.Down | PositionEnum.Right;
                }
                
                if (i == 3 && j == 2)
                {
                   newRail.openDirection = PositionEnum.Top | PositionEnum.Right;
                }
                
                if (i == 4 && j == 4)
                {
                    /*newRail.openDirection = PositionEnum.Top;*/
                    newRail.openDirection = PositionEnum.Top | PositionEnum.Right | PositionEnum.Down | PositionEnum.Left;
                }
                currentPosition += Vector3.right;
            }
            gridItems.Add(newRow);
            currentPosition.x= 0;
            currentPosition += Vector3.back;
        }
    }

    private PositionEnum FetchNeighborRails(Coordinate coordinate)
    {
        return NextRail(coordinate, PositionEnum.Top) | NextRail(coordinate, PositionEnum.Down) |
               NextRail(coordinate, PositionEnum.Left) | NextRail(coordinate, PositionEnum.Right);
    }

    public PositionEnum NextRail(Coordinate coordinate, PositionEnum position)
    {
        var nextRailCoordinate = new Coordinate(coordinate.Row, coordinate.Column);
        switch (position)
        {
            case PositionEnum.Left:
                nextRailCoordinate.Column += 1;
                break;
            case PositionEnum.Right:
                nextRailCoordinate.Column -= 1;
                break;
            case PositionEnum.Top:
                nextRailCoordinate.Row += 1;
                break;
            case PositionEnum.Down:
                nextRailCoordinate.Row -= 1;
                break;
        }

        if (nextRailCoordinate.Row >= 0 && nextRailCoordinate.Row < gridItems.Count && nextRailCoordinate.Column < gridItems[nextRailCoordinate.Row].Count && nextRailCoordinate.Column >= 0)
        {
            var comparedDirections = gridItems[nextRailCoordinate.Row][nextRailCoordinate.Column].openDirection;
            switch (position)
            {
                case PositionEnum.None:
                    return comparedDirections;
                case PositionEnum.Left:
                    return (comparedDirections & PositionEnum.Left) == PositionEnum.Left ? PositionEnum.Right : PositionEnum.None;
                case PositionEnum.Right:
                    return (comparedDirections & PositionEnum.Right) == PositionEnum.Right ? PositionEnum.Left : PositionEnum.None;
                case PositionEnum.Top:
                    return (comparedDirections & PositionEnum.Top)== PositionEnum.Top ? PositionEnum.Down : PositionEnum.None;
                case PositionEnum.Down:
                    return (comparedDirections & PositionEnum.Down)== PositionEnum.Down ? PositionEnum.Top : PositionEnum.None;
                default:
                    return comparedDirections;
            }

        }
        return PositionEnum.None;
    }
    
    public void ConstructRail(Rail requestedRail, Coordinate position)
    {

        var targetRail = gridItems[position.Row][position.Column];
        if (targetRail.isBuildable)
        {
            var gridPosition = targetRail.gameObject.transform.position;
            targetRail.OnRequestOpenRoutesHandler -= FetchNeighborRails;
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
