using UnityEngine;

namespace CarController.SimpleCarController
{
    [System.Serializable]
    public class AxleInfo
    {
        [SerializeField] internal WheelCollider leftWheel;
        [SerializeField] internal GameObject leftWheelVisual;
        [SerializeField] internal WheelCollider rightWheel;
        [SerializeField] internal GameObject rightWheelVisual;
        [SerializeField] internal bool motor;
        [SerializeField] internal bool steering;
    
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
}