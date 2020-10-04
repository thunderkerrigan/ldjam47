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
        if (parentRail)
        {
            parentRail.OnReceiveOpenRouteHandler -= OnUpdateAccess;
        }
    }

    private void OnUpdateAccess(PositionEnum access)
    {
        if ((access & direction) == direction)
        {
            _renderer.material = _openMaterial;
        }
        else
        {
            // _renderer.material = null;
            _renderer.material = _closedMaterial;
        }
    }

}
