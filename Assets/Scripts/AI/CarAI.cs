using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Cars
{
	public class CarAI : MonoBehaviour
	{
		private AdvancedCarController m_carController;
		[SerializeField] private Pid_Controller m_pidController;
		[SerializeField] List<Transform> targetPoints = new List<Transform>();
		private List<WheelCollider> wheels = new List<WheelCollider>();
		private int CountTarget;
		private Vector3 target;
		private Vector3 pastTarget;
		public int I;
		public int pastI;
		public float betweenTargets;
		private float beforestopDist;
		private float stopMultiplayer;
		public float disToPos;
		public float stopDist;
		//[SerializeField] private float stopDistLow;
		public float turnDist;
		//[SerializeField] private float angleDistLow;
		private Rigidbody rb;
		public float stopDistFormula;
		public float carSpeedMS;
		public float carSpeedKMH;
		public float multiplierFromBrake;
		public float multiplierFromacceler;
		private void Awake()
		{
			var points = GameObject.Find("Points").transform;
			for (int i = 0; i < points.childCount; i++)
			{
				targetPoints.Add(points.GetChild(i));
				//targetPoints[i] = points.GetChild(i);
			}
			m_carController = GetComponent<AdvancedCarController>();
			CountTarget = targetPoints.Count;
			rb = GetComponent<Rigidbody>();
		}

		private void Start()
		{
			I = 0;
			target = targetPoints[I].position;
			pastTarget = transform.position;
			betweenTargets = Vector3.Distance(pastTarget, target);
			beforestopDist = Dependence.SetDistansToStop(m_carController.maxSpeed, betweenTargets, stopDist);
			turnDist = Dependence.SetDistansToAngle(m_carController.maxSpeed, betweenTargets, turnDist);
			stopMultiplayer = Dependence.SetMultiplayerFromAcceleration(m_carController.m_accelerationMultiplier);
			stopDist = beforestopDist * stopMultiplayer;
		}

		private void FUpdate()
		{
			disToPos = Vector3.Distance(transform.position, target);
			carSpeedMS = m_carController.carSpeedInAI;

		//	if (disToPos > stopDist)
		//	{
		//		m_carController.GoForward();
		//	}
		//	else if (stopDist > disToPos && disToPos > turnDist)
		//	{
		//		m_carController.Brakes();
		//	}
			if (disToPos > stopDistFormula)
			{ 
				m_carController.GoForward();
			}
			else if (stopDistFormula > disToPos && disToPos > 10)
			{
				//rb.drag = 0.05f;
				m_carController.Brakes();
				for (int i = 0; i < m_carController.m_wheelsCount; i++)
				{
					m_carController.m_wheelColliders[i].motorTorque = 0;
				}
			}
			else
			{
			//	rb.drag = 0;

				SwapTarget();
			}
			m_carController.AnimateWheelMeshes();
		}
		void FixedUpdate()
		{
			FUpdate();
			target.y = transform.position.y;
			var targetDir = (target - transform.position).normalized;
			var forwardDir = transform.rotation * Vector3.forward;

			var currentAngle = Vector3.SignedAngle(Vector3.forward, forwardDir, Vector3.up);
			var targetAngle = Vector3.SignedAngle(Vector3.forward, targetDir, Vector3.up);

			float input = m_pidController.UpdateAngle(Time.fixedDeltaTime, currentAngle, targetAngle);
				m_carController.TurnSide(input);
				carSpeedKMH = carSpeedMS * 3.6f ;
				stopDistFormula = ((float) (multiplierFromacceler * multiplierFromBrake * Math.Pow(carSpeedKMH, 2) / (254 * 0.7)));
		}

            public void SwapTarget()
		{
			if (I < CountTarget)
			{
				I++;
				target = targetPoints[I].position;
				pastI = I - 1;
				pastTarget = targetPoints[pastI].transform.position;
				betweenTargets = Vector3.Distance(pastTarget, target);
				beforestopDist = Dependence.SetDistansToStop(m_carController.maxSpeed, betweenTargets, stopDist);
				turnDist = Dependence.SetDistansToAngle(m_carController.maxSpeed, betweenTargets, turnDist);
				stopDist = beforestopDist * stopMultiplayer;
				
			}
			else
			{
				m_carController.InvokeRepeating("Brakes", 0f, 0.1f);
			}
		}
            
	}
}