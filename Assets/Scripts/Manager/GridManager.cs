using System;
using System.Collections.Generic;
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
        this.DefineSpawnTile();
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
                int reservedTileIndex = this._reservedTiles.FindIndex(c => c.Row == i && c.Column == j);
                if(reservedTileIndex == -1) { 
                    var nextOrientation = PositionEnum.None;
                    var noneRail = filterSinglePrefabsForOrientation(nextOrientation);
                    var newRail = Instantiate(noneRail, currentPosition, Quaternion.identity);
                    newRail.openDirection = nextOrientation;
                    newRail.coordinate = new Coordinate(i,j);
                    newRail.OnRequestOpenRoutesHandler += FetchNeighborRails;
                     newRail.SetText("");

                    newRow.Add(newRail);
				} else { newRow.Add(this._spawnTiles[reservedTileIndex]);}
                
                currentPosition += Vector3.right;
            }
            gridItems.Add(newRow);
            currentPosition.x= 0;
            currentPosition += Vector3.back;
        }
    }

    public Rail filterSinglePrefabsForOrientation(PositionEnum orientation)
    {
        return InstantiableRails.Find((rail => rail.openDirection == orientation));

    }
    
    public Rail filterSpawnPrefabsForOrientation(PositionEnum orientation)
    {
        return this.SpawnRails.Find((rail => rail.openDirection == orientation));

    }
    
    public List<Rail> filterPrefabsForOrientation(PositionEnum orientation)
    {
        return InstantiableRails.FindAll((rail => (rail.openDirection & orientation) == orientation));
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
                Destroy(item);
            }
        }
        gridItems = new List<List<Rail>>(gridSize);
    }

    private void DefineSpawnTile () {
        for (int i = 0; i < this._numberSpawnTile; i++) {
            bool isValideTile = false;
            while (!isValideTile) { 
                int coefX = UnityEngine.Random.Range(0,2);
                int coefY = UnityEngine.Random.Range(0,2);
                int x;
                int y;
                
                // TOP and RIGHT side
                if(coefX  == 1 && coefY  == 1) {
                    // RIDE SIDE
                    if(UnityEngine.Random.Range(0,2) == 0) {                        
                        x = this.gridSize-1;
                        y = UnityEngine.Random.Range(0, this.gridSize-1);
                    // TOP SIDE
					} else {            
                        x = UnityEngine.Random.Range(0, this.gridSize-1);
                        y = this.gridSize-1;
					}
                // LEFT and BOTTOM side
				} else {
                    x = UnityEngine.Random.Range(0, this.gridSize-1) * coefX;
                    y = UnityEngine.Random.Range(0, this.gridSize-1) * coefY;
				}

                if(this._reservedTiles.FindIndex(c => c.Row == y && c.Column == x) == -1) {
                    isValideTile = true;
                    this._reservedTiles.Add(new Coordinate(y, x));
                    
                    var orientation = PositionEnum.None;
                    // LEFT BOTTOM corner
                    if((x == 0 && y == 0)) {
                        if (UnityEngine.Random.Range(0, 2) == 0) { orientation = PositionEnum.Right; }
                        else { orientation = PositionEnum.Down; }
                    // LEFT TOP corner
					} else if((x == 0 && y == this.gridSize-1)) {
                        if (UnityEngine.Random.Range(0, 2) == 0) { orientation = PositionEnum.Right; }
                        else { orientation = PositionEnum.Top; }
					// RIGHT BOTTOM corner
					} else if((x == this.gridSize-1 && y == 0)) {
                        if (UnityEngine.Random.Range(0, 2) == 0) { orientation = PositionEnum.Left; }
                        else { orientation = PositionEnum.Down; }
					// RIGHT TOP corner
					} else if((x == this.gridSize-1 && y == this.gridSize-1)) {
                        if (UnityEngine.Random.Range(0, 2) == 0) { orientation = PositionEnum.Left; }
                        else { orientation = PositionEnum.Top; }
					// LEFT side
					} else if(x == 0) { orientation = PositionEnum.Right; }
					// RIGHT side
					else if(x == this.gridSize-1) { orientation = PositionEnum.Left; }
					// BOTTOM side
					else if(y == 0) { orientation = PositionEnum.Down; }
					// LEFT side
					else if(y == this.gridSize-1) { orientation = PositionEnum.Top; }

                    // Define the position in the grid of the Tile
                    Vector3 tilePosition = (x * Vector3.right) + (y * Vector3.back);
                    // Init the Spawn 
                    Rail noneRail = filterSpawnPrefabsForOrientation(orientation);
                    Rail newRail = Instantiate(noneRail, tilePosition, Quaternion.identity);
                    newRail.openDirection = orientation;
                    newRail.coordinate = new Coordinate(y, x);
                    newRail.OnRequestOpenRoutesHandler += FetchNeighborRails;
                    newRail.SetText("");
                    this._spawnTiles.Add(newRail);
				}
			}
		}
        if (this._spawnManager != null) { this._spawnManager.InitSpawnList(this._spawnTiles); }
    }
    
}
