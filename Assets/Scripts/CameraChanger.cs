using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] virtualCameras;
    [SerializeField] private GameObject[] panels;
    

    public void MoveToSettings()
    {
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].gameObject.SetActive(false);
        }
        virtualCameras[1].gameObject.SetActive(true);
        panels[0].gameObject.SetActive(false);
        panels[1].gameObject.SetActive(true);
        
    }
    

   
 
    public void MoveToMenu()
    {
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].gameObject.SetActive(false);
        }
        virtualCameras[0].gameObject.SetActive(true);
        panels[1].gameObject.SetActive(false);
        panels[0].gameObject.SetActive(true);
    }
}
