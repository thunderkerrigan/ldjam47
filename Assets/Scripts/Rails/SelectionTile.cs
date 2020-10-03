using System;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

public class SelectionTile : MonoBehaviour
{
    public Material GoodMaterial;
    public Material BadMaterial;
    [SerializeField] private MeshRenderer hintRenderer;

    private Rail ParentRail;
    // Start is called before the first frame update
    void Start()
    {
        ParentRail = gameObject.GetComponentInParent<Rail>();
        ParentRail.OnReceiveOpenRouteHandler += SetMaterial;
    }

    private void OnDestroy()
    {
        ParentRail.OnReceiveOpenRouteHandler -= SetMaterial;
    }

    private void SetMaterial(PositionEnum access)
    {
        if (access == PositionEnum.None)
        {
            hintRenderer.material = BadMaterial;
        }
        else
        {
            hintRenderer.material = GoodMaterial;
        }
    }
}
