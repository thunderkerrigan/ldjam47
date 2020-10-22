using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRailState : RailBaseState
{
    public override void EnterState(Rail rail)
    {
        return;
    }

    public override void Update(Rail rail, GameObject rayCastedGameObject)
    {
        rail.isShadowOf.DelegateUpdate(rayCastedGameObject);
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
