using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject spawnPoints;
    public CinemachineVirtualCamera playCamera;
    public GameObject UI;
    private int activeCamera;


    public void Start()
    {
        StaticInfo.SwitchManager = gameObject;
    }

    public void FirstCam()
    {
        var cars = spawnPoints.GetComponentsInChildren<CarInfo>();
        playCamera.Follow = cars[activeCamera].gameObject.transform;
        playCamera.LookAt = cars[activeCamera].gameObject.transform;
        UI.SetActive(true);
        playCamera.gameObject.SetActive(true);
    }


    public void SwitchCamera()
    {
        activeCamera--;
        var cars = spawnPoints.GetComponentsInChildren<CarInfo>();
        if (activeCamera == -1)
            activeCamera = cars.Length - 1;
        for (int i = 0; i < cars.Length; i++)
        {
            playCamera.Follow = cars[activeCamera].gameObject.transform;
            playCamera.LookAt = cars[activeCamera].gameObject.transform;
        }
    }
}
