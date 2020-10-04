using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableRailState : RailBaseState
{
    public override void EnterState(Rail rail)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(Rail rail)
    {
        if (rail.selectionText.activeSelf != rail.isSelectable)
        {
            
            rail.selectionText.SetActive(rail.isSelectable);
            rail.GetOpenRoutes();
        };
    }
}
