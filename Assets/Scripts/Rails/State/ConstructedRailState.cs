using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructedRailState : RailBaseState
{
    public override void EnterState(Rail rail)
    {
        /*Debug.Log("ConstructedRailState"+ rail.coordinate.Row +":" + rail.coordinate.Column);*/
        rail.SetText("ConstructedRailState");
        rail.isSelected = false;
        rail.isBuildable = false;
    }

    public override void Update(Rail rail, GameObject rayCastedGameObject)
    {
        if (rayCastedGameObject == rail.gameObject)
        {
            rail.TransitionToState(rail.SelectedConstructedState);;
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