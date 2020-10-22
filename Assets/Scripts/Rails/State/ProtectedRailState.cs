using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectedRailState : RailBaseState
{
    public override void EnterState(Rail rail)
    {
       // TODO: disable hint + shadowRail
       /*Debug.Log("ProtectedRailState"+ rail.coordinate.Row +":" + rail.coordinate.Column);*/
       rail.SetText("ProtectedRailState");
       rail.isSelected = false;
       rail.isProtected = true;
    }

    public override void Update(Rail rail, GameObject rayCastedGameObject)
    {
        return;
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
