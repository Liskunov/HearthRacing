using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class SwapTarget : ActionNode
{
    private int tagN;
    private int CountTarget;
    protected override void OnStart()
    {
        tagN = blackboard.TegN;
        CountTarget = blackboard.targetPoints.Count;

    }

    protected override void OnStop() 
    {
    }

    protected override State OnUpdate() 
    {
        if (tagN == CountTarget)
        {
            return State.Failure;
        }
        if (tagN < CountTarget)
        {
            blackboard.TegN++;
            blackboard.target = blackboard.targetPoints[tagN].transform.position;
        }

        return State.Failure;
    }
}
