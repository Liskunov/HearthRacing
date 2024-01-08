using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class MaxSpeedSearch : ActionNode
{
    private int m_CarMaxSpeed;
    public int branchMaxSpeed;
    protected override void OnStart()
    {
        m_CarMaxSpeed = context.carController.maxSpeed;
    }

    protected override void OnStop() 
    {
    }

    protected override State OnUpdate() 
    {
        if (m_CarMaxSpeed >= branchMaxSpeed)
        {
            return State.Failure;
        }
        return State.Success;
    }
}
