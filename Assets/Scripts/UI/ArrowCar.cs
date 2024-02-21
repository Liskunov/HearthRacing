using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCar : MonoBehaviour
{
    private Transform m_cameraTransform;

    void Start()
    {
        m_cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        //transform.rotation.z = -90;
        transform.LookAt(m_cameraTransform);
    }
}
