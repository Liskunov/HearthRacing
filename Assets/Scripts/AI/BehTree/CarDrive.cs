using System.Collections;
using System.Collections.Generic;
using Cars;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CarDrive : ActionNode
{
    public float branchMinDist;
    private GameObject car;
    private AdvancedCarController CarController;
    private Vector3 target;
    protected override void OnStart()
    {
        car = context.gameObject;
        CarController = context.carController;
        target = blackboard.target;
    }

    protected override void OnStop() 
    {
        
    }

    protected override State OnUpdate() 
    {
        float disToPos = Vector3.Distance(car.transform.position, target);

        if (disToPos > branchMinDist)
        {
            CarController.GoForward();
        }
        return State.Success;
    }
}
