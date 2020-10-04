using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class RailBaseState
{
    public abstract void EnterState(Rail rail);

    public abstract void Update(Rail rail);

}
