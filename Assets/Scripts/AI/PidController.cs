using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PidController : MonoBehaviour
{
    private float error_old = 0f;
    private float error_sum = 0f;
    
    public float P = 70; //70
    public float I = 0.01f; //0.01
    public float D = 50; //50
    
    public float GetNewValue(float error)
    {
        float alpha = 0f;
        alpha = -P * error;
        error_sum = Helper.AddValueToAverage(error_sum, Time.deltaTime * error, 1000f);
        alpha -= I * error_sum; 
        float d_dt = (error - error_old) / Time.deltaTime;
        alpha -= D * d_dt;
        error_old = error;
        return alpha; 
    }
}
