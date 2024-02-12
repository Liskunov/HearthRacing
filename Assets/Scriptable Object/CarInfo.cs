using System.Collections;
using System.Collections.Generic;
using Cars;
using UnityEngine;

public class CarInfo : MonoBehaviour
{
    
    public List<int> specificationsC = new List<int>();
    public string carName;
    public Sprite carImage;
    


    public void LoadMod()
    {
        gameObject.GetComponent<AdvancedCarController>().m_maxSpeed = specificationsC[0];
        gameObject.GetComponent<AdvancedCarController>().m_accelerationMultiplier = specificationsC[1];
        gameObject.GetComponent<AdvancedCarController>().m_brakeForce = specificationsC[2];
    }
}
