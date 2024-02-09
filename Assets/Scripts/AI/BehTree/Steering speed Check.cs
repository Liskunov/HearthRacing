using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class SteeringspeedCheck : ActionNode
{
    private float CarSteeringSpeed;
    public float branchMinSteerSpeed;
    
    protected override void OnStart()
    {
    }

    protected override void OnStop() 
    {
    }

    protected override State OnUpdate() 
    {
        if (CarSteeringSpeed > branchMinSteerSpeed)
        {
            return State.Failure;
        }
        return State.Success;
    }
}
