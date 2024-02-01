using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        Vector3 waypointFrom = targetPoints[targetIndex - 1].transform.position;
        Vector3 waypointTo = targetPoints[targetIndex].transform.position;
        car = gameObject.GetComponent<Rigidbody>();
    }


    void Update()
    {
        CalculateSteeringAngle();
        
    }

    private void CalculateSteeringAngle()
    {
        carPos = car.transform.position;
        float error = CalculateError(carPos, waypointFrom, waypointTo);
        steeringAngle = pidController.GetNewValue(error);
    }

    private float CalculateError(Vector3 carPos, Vector3 wp_from, Vector3 wp_to)
    {
        Vector2 from = new Vector2(wp_from.x, wp_from.y);
        Vector2 to = new Vector2(wp_to.x, wp_to.y);
        Vector2 pos = new Vector2(carPos.x, carPos.y);
        Vector2 closestPos2D = Helper.GetClosestPointOnLine(from, to, pos);
        
        Vector3 progCoord = new Vector3(closestPos2D.x, 0f, closestPos2D.y);

        Vector3 progressCoordinate = progCoord;
        float error = (carPos - progressCoordinate).magnitude;
        
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
}
