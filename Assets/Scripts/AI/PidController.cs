using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PidController : MonoBehaviour
{
    private float error_old = 0f;
    private float error_sum = 0f;
    
    public float GetNewValue(float error, PID_Parameters pid_parameters)
    {
        float alpha = 0f;
        alpha = -pid_parameters.P * error;
        error_sum = Helper.AddValueToAverage(error_sum, Time.deltaTime * error, 1000f);
        alpha -= pid_parameters.I * error_sum; 
        float d_dt = (error - error_old) / Time.deltaTime;
        alpha -= pid_parameters.D * d_dt;
        error_old = error;
        return alpha; 
    }

    
    
	
    [System.Serializable]
    public struct PID_Parameters
    {
        public float P; //70
        public float I; //0.01
        public float D; //50
    
        public PID_Parameters(float P, float I, float D)
        { 
		 this.P = P; 
		 this.I = I; 
		 this.D = D;
        }
	}
}
