using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pid_Controller    : MonoBehaviour
{
    private float proportionalGain = 1.5f;
    private float integralGain = 1;
    private float derivativeGain = 1;

    private float outputMin = -0.1f;
    private float outputMax = 0.1f;
    private float integralSaturation = 1;

    private float valueLast;
    private float errorLast;
    private float integrationStored;
    private float velocity;  //only used for the info display
    private bool derivativeInitialized;

    float AngleDifference(float a, float b) 
    {
        return (a - b + 540) % 360 - 180;   //calculate modular difference, and remap to [-180, 180]
    }
    
    public float UpdateAngle(float dt, float currentAngle, float targetAngle) 
    {
        if (dt <= 0) throw new ArgumentOutOfRangeException(nameof(dt));
        float error = AngleDifference(targetAngle, currentAngle);

        //calculate P term
        float P = proportionalGain * error;

        //calculate I term
        integrationStored = Mathf.Clamp(integrationStored + (error * dt), -integralSaturation, integralSaturation);
        float I = integralGain * integrationStored;

        //calculate both D terms
        errorLast = error;

        float valueRateOfChange = AngleDifference(currentAngle, valueLast) / dt;
        valueLast = currentAngle;
        velocity = valueRateOfChange;

        //choose D term to use
        float deriveMeasure = 0;
        
        deriveMeasure = -valueRateOfChange;


        float D = derivativeGain * deriveMeasure;

        float result = P + I + D;

        return Mathf.Clamp(result, outputMin, outputMax);
    }
}
