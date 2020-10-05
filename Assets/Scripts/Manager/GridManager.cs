using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public int gridSize = 10;
    public List<Rail> InstantiableRails;
    public SpawnManager _spawnManager;
    public List<Rail> SpawnRails;
    [Range(1f, 10f)]
    public int _numberSpawnTile = 3;
    
    private List<List<Rail>> gridItems;
    private List<Rail> _spawnTiles = new List<Rail>();    
    private List<Coordinate> _reservedTiles = new List<Coordinate>();
    
    
    public List<Rail> SpawnTiles {
        get { return this._spawnTiles; }
    }

    public List<Coordinate> ReservedTiles {
        get { return this._reservedTiles; }
    }

    // Start is called before the first frame update
    void Start()
    {
        gridItems = new List<List<Rail>>(gridSize);
        InitGrid();
    }

    public void InitGrid()
    {
        ClearGrid();
        Vector3 currentPosition = new Vector3();
        for (int i = 0; i < gridSize; i++)
        {
            var newRow = new List<Rail>();
            for (int j = 0; j < gridSize; j++)
            {
                
                    var nextOrientation = PositionEnum.None;
                    var noneRail = filterSinglePrefabsForOrientation(nextOrientation);
                    var newRail = Instantiate(noneRail, currentPosition, Quaternion.identity);
                    newRail.openDirection = nextOrientation;
                    newRail.coordinate = new Coordinate(i,j);
                    newRail.OnRequestOpenRoutesHandler += FetchNeighborRails;
                     newRail.SetText("");

                    newRow.Add(newRail);
				
                
                currentPosition += Vector3.right;
            }
            gridItems.Add(newRow);
            currentPosition.x= 0;
            currentPosition += Vector3.back;
        }

        DefineSpawnTile();
    }

    public Rail filterSinglePrefabsForOrientation(PositionEnum orientation)
    {
        return InstantiableRails.Find((rail => rail.openDirection == orientation));

    }

    public List<Rail> filterPrefabsForOrientation(PositionEnum orientation)
    {
        return InstantiableRails.FindAll((rail => (rail.openDirection & orientation) == orientation));
    }
    
    public List<Rail> filterSpawnPrefabsForOrientation(PositionEnum orientation)
    {
        return SpawnRails.FindAll((rail => (rail.openDirection & orientation) != PositionEnum.None));
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

    private Rail MakeRail(Rail rail, PositionEnum nextOrientation, Vector3 position, Coordinate coordinate)
    {
        var newRail = Instantiate(rail, position, Quaternion.identity);
        newRail.openDirection = rail.openDirection;
        newRail.coordinate = new Coordinate(coordinate.Row,coordinate.Column);
        newRail.OnRequestOpenRoutesHandler += FetchNeighborRails;
        newRail.SetText("");
        return newRail;
    }

    public void DestroyRail(Coordinate position)
    {
        var targetRail = gridItems[position.Row][position.Column];
        if (!targetRail.isProtected)
        {
            var gridPosition = targetRail.gameObject.transform.position;
            Destroy(targetRail.gameObject);
            var emptyRail = filterSinglePrefabsForOrientation(PositionEnum.None);
            var newRail = MakeRail(emptyRail, PositionEnum.None, gridPosition, position);
            gridItems[position.Row][position.Column] = newRail;
        }
    }
    
    public void ConstructRail(Rail requestedRail, Coordinate position)
    {

        var targetRail = gridItems[position.Row][position.Column];
        if (targetRail.isBuildable)
        {
            var gridPosition = targetRail.gameObject.transform.position;
            targetRail.OnRequestOpenRoutesHandler -= FetchNeighborRails;
            Destroy(targetRail.gameObject);
            var newRail = MakeRail(requestedRail, PositionEnum.None, gridPosition, position);
            gridItems[position.Row][position.Column] = newRail;
        }
    }

    private void ClearGrid()
    {
        foreach (var row in gridItems)
        {
            foreach (var item in row)
            {
                Destroy(item.gameObject);
            }
        }
        gridItems = new List<List<Rail>>(gridSize);
    }

    private void DefineSpawnTile () {
        for (int i = 0; i < this._numberSpawnTile; i++)
        {
            var maxRow = gridItems.Count - 1;
            var randomRow = Random.Range(0, maxRow);
            var maxColumn = gridItems[randomRow].Count - 1;
            var randomColumn = Random.Range(0, maxColumn);
            var nextCoordinate = new Coordinate(randomRow, randomColumn);
            var directions = PositionEnum.Top | PositionEnum.Down | PositionEnum.Right | PositionEnum.Left;
            if (nextCoordinate.Row == 0)
            {
                directions ^= PositionEnum.Top;
            }

            if (nextCoordinate.Row == maxRow)
            {
                directions ^= PositionEnum.Down;
            }
            
            if (nextCoordinate.Column == 0)
            {
                directions ^= PositionEnum.Left;
            }
            
            if (nextCoordinate.Column == maxColumn)
            {
                directions ^= PositionEnum.Right;
            }

            var availableSpawnerPrefab = filterSpawnPrefabsForOrientation(directions);
            if (availableSpawnerPrefab.Count > 0)
            {
                var nextSpawnerPrefab = availableSpawnerPrefab[Random.Range(0, availableSpawnerPrefab.Count)];
                ConstructRail(nextSpawnerPrefab,nextCoordinate); 
                _spawnTiles.Add(gridItems[nextCoordinate.Row][nextCoordinate.Column]);
            }
            
        }
        if (this._spawnManager != null) { this._spawnManager.InitSpawnList(this._spawnTiles); }
    }
    
}
