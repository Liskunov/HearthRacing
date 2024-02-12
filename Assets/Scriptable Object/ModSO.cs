using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModInfo", menuName = "ModSO")]
public class ModSO : ScriptableObject
{
    public string name;
    public Sprite icon;
    public int price = 0;
    public float ratingMod = 0.1f;
    
    [Range(0, 190)] public int MaxSpeed = 0;
    [Range(0, 12)] public int AccelerationMultiplier = 0;
    [Range(0, 600)] public int BrakeForce = 0;
    
    public string Name
    {
        get
        {
            return name;
        }
    }

    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }

   
    public int Price
    {
        get
        {
            return price;
        }
    }
   
    public float RatingMod
    {
        get
        {
            return ratingMod;
        }
    }
}
