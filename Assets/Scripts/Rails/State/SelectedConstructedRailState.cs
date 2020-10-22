using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.Soundy;
using UnityEngine;

public class SelectedConstructedRailState : RailBaseState
{
    public override void EnterState(Rail rail)
    {
        // TODO: Draw destructTile 
        /*Debug.Log("SelectedConstructedRailState"+ rail.coordinate.Row +":" + rail.coordinate.Column);*/
        rail.SetText("SelectedConstructedRailState");
        rail.isSelected = true;
    }

    public override void Update(Rail rail, GameObject rayCastedGameObject)
    {
        if (rayCastedGameObject != rail.gameObject)
        {
            rail.TransitionToState(rail.ConstructedState);
        }
    }

    public override void HandleScroll(Rail rail, int offset)
    {
        return;
    }

    public override void HandleClick(Rail rail)
    {
        // TODO: perform destruction + play sound + enterState SelectedBuildable
        SoundyManager.Play("Game", "deconstruct", rail.gameObject.transform.position);
        rail.gridManager.DestroyRail(rail.coordinate);
        rail.isBuildable = true;
    }
}
