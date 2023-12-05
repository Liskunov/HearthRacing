using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] virtualCameras;
    private int currentCameraIndex;

    public void MoveToSettings()
    {
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].gameObject.SetActive(false);
        }
        virtualCameras[1].gameObject.SetActive(true);
    }
    

    private void SwitchCamera()
    {
        virtualCameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex++;

        if (currentCameraIndex >= virtualCameras.Length)
            currentCameraIndex = 0;
        
        virtualCameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
