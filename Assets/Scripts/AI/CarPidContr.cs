using System.Collections;
using System.Collections.Generic;
using Cars;
using TheKiwiCoder;
using UnityEngine;

public class CarPidContr : MonoBehaviour
{
    private Blackboard m_blackboard;
    private Pid_Controller m_pidController;
    private AdvancedCarController m_carController;
    void Awake()
    {
        m_carController = GetComponent<AdvancedCarController>();
        m_pidController = GetComponent<Pid_Controller>();
        m_blackboard = new Blackboard();
    }


    void Update()
    {
        var targetPosition = m_blackboard.target;
        targetPosition.y = transform.position.y;
        var targetDir = (targetPosition - transform.position).normalized;
        var forwardDir = transform.rotation * Vector3.forward;

        var currentAngle = Vector3.SignedAngle(Vector3.forward, forwardDir, Vector3.up);
        var targetAngle = Vector3.SignedAngle(Vector3.forward, targetDir, Vector3.up);

        float input = m_pidController.UpdateAngle(Time.fixedDeltaTime, currentAngle, targetAngle);
        m_carController.TurnSide(input);
    }
}
