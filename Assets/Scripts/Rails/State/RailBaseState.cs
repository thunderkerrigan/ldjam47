using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class RailBaseState
{
    public abstract void EnterState(Rail rail);

    public abstract void Update(Rail rail, GameObject rayCastedGameObject);

    public abstract void HandleScroll(Rail rail, int offset);

    public abstract void HandleClick(Rail rail);

}
