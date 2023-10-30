using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public GameObject leftWheelVisual;
    public WheelCollider rightWheel;
    public GameObject rightWheelVisual;
    public bool motor;
    public bool steering;

    public void ApplyLocalPositionToVisuals()
    {
        if (leftWheelVisual == null)
        {
            return;
        }

        leftWheel.GetWorldPose(out Vector3 position, out Quaternion rotation);
        leftWheelVisual.transform.position = position;
        leftWheelVisual.transform.rotation = rotation;

        if (rightWheelVisual == null)
        {
            return;
        }

        rightWheel.GetWorldPose(out position, out rotation);
        rightWheelVisual.transform.position = position;
        rightWheelVisual.transform.rotation = rotation;
    }
}

public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
 

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            axleInfo.ApplyLocalPositionToVisuals();
        }
    }
}