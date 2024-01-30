using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public static Vector2 GetClosestPointOnLine(Vector2 a, Vector2 b, Vector2 p)
    {
        Vector2 a_p = p - a;
        Vector2 a_b = b - a;

        float sqrMagnitudeAB = a_b.sqrMagnitude;

        float ABAPproduct = Vector2.Dot(a_p, a_b);

        float distance = ABAPproduct / sqrMagnitudeAB;

        return a + a_b * distance;
    }
    
    public static float AddValueToAverage(float oldAverage, float valueToAdd, float count)
    {
        float newAverage = ((oldAverage * count) + valueToAdd) / (count + 1f); 
        return newAverage;
    }
}
