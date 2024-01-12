using Cars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public CarAI carAI;
    public GameObject car;
    public GameObject startPoint;
    public GameObject targetN1;
    private Rigidbody IK;

    private void Awake()
    {
        carAI = car.GetComponent<CarAI>();
        IK = car.GetComponent<Rigidbody>();
    }
    public void Reset()
    {
        carAI.I = 0;
        carAI.Target = targetN1.transform.position;
        car.transform.position = startPoint.transform.position;
        car.transform.rotation = startPoint.transform.rotation;
        IK.isKinematic = true;
        IK.isKinematic = false;
    }
}
