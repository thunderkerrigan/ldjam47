using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableRailState : RailBaseState
{
    public override void EnterState(Rail rail)
    {
        /*Debug.Log("BuildableRailState"+ rail.coordinate.Row +":" + rail.coordinate.Column);*/
        rail.SetText("BuildableRailState");
        rail.isBuildable = true;
        rail.isSelected = false;
    }

    public override void Update(Rail rail, GameObject rayCastedGameObject)
    {
        if (rayCastedGameObject == rail.gameObject)
        {
            rail.TransitionToState(rail.SelectedBuildableState);
        }
    }

    public override void HandleScroll(Rail rail, int offset)
    {
        return;
    }

    public override void HandleClick(Rail rail)
    {
        return;
    }
}
