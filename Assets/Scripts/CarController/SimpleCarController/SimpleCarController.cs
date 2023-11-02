using UnityEngine;
using System.Collections.Generic;

namespace CarController.SimpleCarController
{
    public class SimpleCarController : MonoBehaviour
    {
        [SerializeField] private List<AxleInfo> m_axleInfos;
        [SerializeField] private float m_maxMotorTorque;
        [SerializeField] private float m_maxSteeringAngle;

        public void FixedUpdate()
        {
            float motor = m_maxMotorTorque * Input.GetAxis("Vertical");
            float steering = m_maxSteeringAngle * Input.GetAxis("Horizontal");
    
            foreach (AxleInfo axleInfo in m_axleInfos)
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
}
