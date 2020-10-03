using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    [SerializeField] private Material _closedMaterial;
    [SerializeField] private Material _openMaterial;
    [SerializeField] private Material _testMaterial;
    public PositionEnum direction;
    private Rail parentRail;
    private MeshRenderer _renderer;
    private bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        parentRail = gameObject.GetComponentInParent<Rail>();
        _renderer = gameObject.GetComponent<MeshRenderer>();
        parentRail.OnReceiveOpenRouteHandler += OnUpdateAccess;

    }

    private void OnDestroy()
    {
        parentRail.OnReceiveOpenRouteHandler -= OnUpdateAccess;
    }

    private void OnUpdateAccess(PositionEnum access)
    {
        if ((parentRail.openDirection & direction) == direction)
        {
            _renderer.material = _testMaterial;
            return;
        }

        if (access != PositionEnum.None)
        {
            Debug.Log("receive position!"+ access + " for " + parentRail.coordinate.Row + " ," +parentRail.coordinate.Column);
        }
        if ((access & direction) == direction)
        {
            _renderer.material = _openMaterial;
        }
        else
        {
            _renderer.material = _closedMaterial;
        }
    }

}
