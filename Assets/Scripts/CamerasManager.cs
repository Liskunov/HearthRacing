using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static class CameraNames
    {
        public const string Menu = "MenuCamera";
        public const string Start = "StartCamera";
        public const string Catalog = "CatalogCamera";
        public const string Shop = "ShopCamera";
        public const string Settings = "SettingsCamera";
    }
    
    [SerializeField] private List<CinemachineVirtualCamera> m_cameras;
    [SerializeField] private string m_startCamera;

    private void Awake()
    {
        Activate(m_startCamera);
    }

    public void Activate(string cameraName)
    {
        foreach (var cam in m_cameras)
        {
            cam.enabled = cameraName == cam.name;
        }
    }
}