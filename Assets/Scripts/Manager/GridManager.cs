using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum PositionEnum
{
    Left,
    Right,
    Top,
    Down
}
public class GridManager : MonoBehaviour
{

    public GameObject EmptyRailItem;
    private List<List<GameObject>> gridItems;
    
    // Start is called before the first frame update
    void Start()
    {
        gridItems = new List<List<GameObject>>(10);
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
            var newRow = new List<GameObject>();
            for (int j = 0; j < 10; j++)
            {
                var newRail = Instantiate(EmptyRailItem, currentPosition, Quaternion.identity);
                newRow.Add(newRail);
                currentPosition += Vector3.right;
            }
            gridItems.Add(newRow);
            currentPosition.x = 0;
            currentPosition += Vector3.back;
        }
    }

    /*
    public GameObject NextRail(GameObject requestingRail, PositionEnum position)
    {
        switch (position)
        {
            case PositionEnum.Left:
                break;
            case PositionEnum.Right:
                break;
            case PositionEnum.Top:
                break;
            case PositionEnum.Down:
                break;
        }
    }
    */

    private void ClearGrid()
    {
        foreach (var row in gridItems)
        {
            foreach (var item in row)
            {
                Destroy(item);
            }
        }
        gridItems = new List<List<GameObject>>(10);
    }
    
}
