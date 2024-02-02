using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInfo : MonoBehaviour
{
    
    public List<float> specificationsInt = new List<float>();
    
    private void Start()
    {
        specificationsInt.Add(0);
        specificationsInt.Add(0);
        specificationsInt.Add(0);
        specificationsInt.Add(0);
        specificationsInt.Add(0);
    }

}
