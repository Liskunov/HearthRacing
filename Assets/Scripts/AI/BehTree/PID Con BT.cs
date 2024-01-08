using System.Collections;
using System.Collections.Generic;
using Cars;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class PIDConBT : ActionNode
{
    private GameObject car;
    private Pid_Controller pidController;
    private AdvancedCarController carController;
    protected override void OnStart()
    {
        car = context.gameObject;
        pidController = context.pidCon;
        carController = context.carController;
        
    }

    protected override void OnStop() 
    {
    }

    protected override State OnUpdate() 
    {
        var targetPosition = blackboard.target;
        targetPosition.y = car.transform.position.y;
        var targetDir = (targetPosition - car.transform.position).normalized;
        var forwardDir = car.transform.rotation * Vector3.forward;

        var currentAngle = Vector3.SignedAngle(Vector3.forward, forwardDir, Vector3.up);
        var targetAngle = Vector3.SignedAngle(Vector3.forward, targetDir, Vector3.up);

        float input = pidController.UpdateAngle(Time.fixedDeltaTime, currentAngle, targetAngle);
        carController.TurnSide(input);
        return State.Success;
    }
}
