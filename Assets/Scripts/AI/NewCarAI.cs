using System;
using System.Collections;
using System.Collections.Generic;
using Cars;
using Unity.VisualScripting;
using UnityEngine;
using FixedUpdate = UnityEngine.PlayerLoop.FixedUpdate;

public class NewCarAI : MonoBehaviour
{
    private int targetIndex = 1;
    private Rigidbody car;
    private Vector3 carPos;
    [SerializeField] List<GameObject> targetPoints = new List<GameObject>();
    private Vector3 waypointFrom;
    private Vector3 waypointTo;
    private float steeringAngle;
    PidController pidController;
    private AdvancedCarController carController;
    [SerializeField] private float stopDist;
    [SerializeField] private float angleDist;

    void Start()
    {
        pidController = gameObject.GetComponent<PidController>();
        carController = gameObject.GetComponent<AdvancedCarController>();
        NextTarget();
        car = gameObject.GetComponent<Rigidbody>();
    }


    void Update()
    {
        float disToPos = Vector3.Distance(car.transform.position, waypointTo);
        
        //CalculateSteeringAngle();
        
        if (disToPos > stopDist)
        {
            carController.GoForward();
        }
        else if (stopDist > disToPos && disToPos > angleDist)
        {
            carController.Brakes();
        }
        else
        {
            SwapTarget();
        }
        carController.AnimateWheelMeshes();
        
    }

    private void FixedUpdate()
    {
        var targetPosition = waypointTo;
        targetPosition.y = transform.position.y;
        var targetDir = (targetPosition - transform.position).normalized;
        var forwardDir = transform.rotation * Vector3.forward;

        var currentAngle = Vector3.SignedAngle(Vector3.forward, forwardDir, Vector3.up);
        var targetAngle = Vector3.SignedAngle(Vector3.forward, targetDir, Vector3.up);
        float error = (targetAngle - currentAngle + 540) % 360 - 180;
        steeringAngle = pidController.GetNewValue(error);
        Debug.Log(steeringAngle);
        carController.TurnSide(steeringAngle);
    }

    private void NextTarget()
    {
        waypointFrom = targetPoints[targetIndex - 1].transform.position;
        waypointTo = targetPoints[targetIndex].transform.position;
    }

    private void CalculateSteeringAngle()
    {
        carPos = car.transform.position;
        float error = CalculateError(carPos, waypointFrom, waypointTo);
        //Debug.Log(error);
        steeringAngle = pidController.GetNewValue(error);
        //Debug.Log(steeringAngle);
        carController.TurnSideAI(steeringAngle);
    }

    private float CalculateError(Vector3 carPos, Vector3 wp_from, Vector3 wp_to)
    {
        //Debug.Log(carPos);
        //Debug.Log(wp_from);
        //Debug.Log(wp_to);

        Vector2 from = new Vector2(wp_from.x, wp_from.y);
        Vector2 to = new Vector2(wp_to.x, wp_to.y);
        Vector2 pos = new Vector2(carPos.x, carPos.y);
        Vector2 closestPos2D = Helper.GetClosestPointOnLine(from, to, pos);

        Vector3 progCoord = new Vector3(closestPos2D.x, 0f, closestPos2D.y);

        Vector3 progressCoordinate = progCoord;
        Debug.Log(progressCoordinate);
        Debug.Log(carPos);
        float error = (carPos - progressCoordinate).magnitude;
        Debug.Log(error);
        
        Vector3 toCarVec = carPos - wp_from;
        Vector3 toWaypointVec = wp_to - wp_from;

        Vector3 perp = Vector3.Cross(toCarVec, toWaypointVec);

        float dir = Vector3.Dot(perp, Vector3.up);

        //The car is right of the waypoint
        if (dir > 0f)
        {
            error *= -1f;
        }
        
        
        return error;

    }
    public void SwapTarget()
    {
        if (targetIndex == targetPoints.Count)
        {
            carController.InvokeRepeating("Brakes", 0f, 0.1f);
        }
        else
        {
            targetIndex++;
            NextTarget();
        }
    }
}
