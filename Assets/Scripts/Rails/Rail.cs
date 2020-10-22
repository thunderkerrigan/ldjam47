using System;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public enum EnemyBehavior
{
    Buildable,
    Constructed,
    Shadow
}

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

public delegate void OnDeathHandler();


public class Rail : MonoBehaviour
{
    public event RequestOpenRoutesHandler OnRequestOpenRoutesHandler;
    public event ReceiveOpenRouteHandler OnReceiveOpenRouteHandler;
    
    public event OnDeathHandler OnDeathHandler;

    public Coordinate coordinate;    
    [SerializeField] public GameObject selectionText;
    [SerializeField] public List<Material> backgroundMaterials;
    public bool isSelected = false;
    public bool isBuildable = true;
    public bool isProtected = false;
    public PositionEnum openDirection = PositionEnum.None;
    public TextMeshPro textMesh;
    public MeshRenderer backgroundTile;
    public GridManager gridManager;
    public MouseController mouseController;
    
    private List<GameObject> _trainsOnRail = new List<GameObject>(); 

    #region State

    public readonly BuildableRailState BuildableState = new BuildableRailState();
    public readonly ConstructedRailState ConstructedState = new ConstructedRailState();
    public  readonly SelectedBuildableRailState SelectedBuildableState = new SelectedBuildableRailState();
    public  readonly SelectedConstructedRailState SelectedConstructedState = new SelectedConstructedRailState();
    public readonly  ProtectedRailState ProtectedState = new ProtectedRailState();
    public readonly  ShadowRailState ShadowState = new ShadowRailState();

    public Rail isShadowOf;
    private RailBaseState _currentState;

    #endregion

    private void Awake()
    {
        var id = Random.Range(0, backgroundMaterials.Count);
        backgroundTile.material = backgroundMaterials[id];
        gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
        mouseController = GameObject.FindGameObjectWithTag("MouseController").GetComponent<MouseController>();
        mouseController.OnScroll += HandleScroll;
        mouseController.OnClick += HandleClick;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isShadowOf != null)
        {
            TransitionToState(ShadowState);
            return;
        }
        if (isProtected)
        {
            TransitionToState(ProtectedState);
            return;
        }
        if (openDirection == PositionEnum.None)
        {
            TransitionToState(BuildableState);
        }
        else
        {
            TransitionToState(ConstructedState);
        }
    }

    
    public void TransitionToState(RailBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }
    public void SetText(string text)
    {
        textMesh.SetText(text);
    }


    // Update is called once per frame
    void Update()
    {
        // CurrentState.Update(this);
        if (selectionText.activeSelf != isSelected)
        {
            selectionText.SetActive(isSelected);
            GetOpenRoutes();
        };
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!isProtected)
        {
            if (Physics.Raycast(ray, out var _hitData, 1000))
            {
                var selectedObject = _hitData.transform.gameObject;
                _currentState.Update(this, selectedObject);
            }
        }
    }

    public void DelegateUpdate(GameObject raycastedGameObject)
    {
        _currentState.Update(this, raycastedGameObject);
    }

    public void AddTrain(GameObject train)
    {
        _trainsOnRail.Add(train);
        isProtected = true;
    }
    public void RemoveTrain(GameObject train)
    {
        _trainsOnRail.Remove(train);
        isProtected = _trainsOnRail.Count> 0;
    }
    

    public void HandleScroll(int offset)
    {
        _currentState.HandleScroll(this,offset);
    }

    public void HandleClick()
    {
        _currentState.HandleClick(this);
    }
    
    public PositionEnum GetOpenRoutes()
    {
        if (OnRequestOpenRoutesHandler != null)
        {
           var openAccess = OnRequestOpenRoutesHandler(this.coordinate);
           if (OnReceiveOpenRouteHandler != null)
           {
               OnReceiveOpenRouteHandler(openAccess);
               return openAccess;
           }
        }

        return PositionEnum.None;
    }

    public void SelfDestroy()
    {
        mouseController.OnScroll -= HandleScroll;
        mouseController.OnClick -= HandleClick;
        if (OnDeathHandler != null)
        {
            OnDeathHandler();
        }
        Destroy(gameObject);
    }
}
