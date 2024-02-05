using System.Collections;
using System.Collections.Generic;
using Cars;
using UnityEngine;

public class CarInfo : MonoBehaviour
{
    
    public List<int> specifications = new List<int>();

    public void GetMod()
    {
        Debug.Log(specifications[0]);
        Debug.Log(specifications[1]);
        Debug.Log(specifications[2]);
        gameObject.GetComponent<AdvancedCarController>().m_maxSpeed = specifications[0];
        gameObject.GetComponent<AdvancedCarController>().m_accelerationMultiplier = specifications[1];
        gameObject.GetComponent<AdvancedCarController>().m_brakeForce = specifications[2];
    }
}
