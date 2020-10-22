using System;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

public class SelectionTile : MonoBehaviour
{
    public Material DestroyableMaterial;
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
        if (ParentRail)
        {
            ParentRail.OnReceiveOpenRouteHandler -= SetMaterial;
        }
    }

    private void SetMaterial(PositionEnum access)
    {
        Debug.Log("coucou");
        if (access == PositionEnum.None)
        {
            hintRenderer.material = BadMaterial;
        }
        else
        {
            if (ParentRail.isBuildable)
            {
                hintRenderer.material = GoodMaterial;
            }
            else
            {
                if (!ParentRail.isProtected && !ParentRail.isBuildable)
                {
                    hintRenderer.material = DestroyableMaterial;
                }
            }
        }
    }
}
