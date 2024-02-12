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
		private Pid_Controller m_pidController;
		List<Transform> targetPoints = new List<Transform>();
		private int CountTarget;
		private Vector3 target;
		private Vector3 pastTarget;
		public int I;
		public int pastI;
		private float betweenTargets;
		public float disToPos;
		public float stopDistFormula;
		public float carSpeedMS;
		public float carSpeedKMH;
		public float multiplierFromBrake;
		public float multiplierFromAcceler;
		public float acceler;
		public int brakeFors;

		private void Awake()
		{
			var points = GameObject.Find("Points").transform;
			for (int i = 0; i < points.childCount; i++)
			{
				targetPoints.Add(points.GetChild(i));
			}
			m_carController = GetComponent<AdvancedCarController>();
			m_pidController = GetComponent<Pid_Controller>();
			CountTarget = targetPoints.Count;
			acceler = m_carController.m_accelerationMultiplier;
			brakeFors = m_carController.m_brakeForce;
		}

		private void Start()
		{
			I = 0;
			target = targetPoints[I].position;
			pastTarget = transform.position;
			betweenTargets = Vector3.Distance(pastTarget, target);
			multiplierFromAcceler = CalculateAccelerMult(acceler);
			multiplierFromBrake = CalculateBrakeMult();
		}

		private void FUpdate()
		{
			disToPos = Vector3.Distance(transform.position, target);
			carSpeedMS = m_carController.carSpeedInAI;
			
			if (disToPos > stopDistFormula)
			{ 
				m_carController.GoForward();
			}
			else if (stopDistFormula > disToPos && disToPos > 10)
			{
				m_carController.Brakes();
				for (int i = 0; i < m_carController.m_wheelsCount; i++)
				{
					m_carController.m_wheelColliders[i].motorTorque = 0;
				}
			}
			else
			{
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
				stopDistFormula = ((float) (multiplierFromAcceler * multiplierFromBrake * Math.Pow(carSpeedKMH, 2) / (254 * 0.7)));
		}

		public float CalculateAccelerMult(float acceler)
		{
			float accelerMult = 1 + (0.05f * (acceler - 4));
			return accelerMult;
		}

		public float CalculateBrakeMult()
		{
			switch (brakeFors)
			{
				case 200:
					multiplierFromBrake = 1.5f; //1.4f;
					break;
				case 250:
					multiplierFromBrake = 1.4f; //1.25f;
					break;
				case 300:
					multiplierFromBrake = 1.35f; //1.35f;
					break;
				case 350:
					multiplierFromBrake = 1.3f; //1.15f;
					break;
				case 400:
					multiplierFromBrake = 1.25f; //1.2f;
					break;
				case 450:
					multiplierFromBrake = 1.2f; //1.1f;
					break;
				case 500:
					multiplierFromBrake = 1.1f; //1.05f;
					break;
				case 550:
					multiplierFromBrake = 1f; //1f;
					break;
				case 600:
					multiplierFromBrake = 0.9f; //0.95f;
					break;
			}
			return multiplierFromBrake;
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
				
			}
			else
			{
				m_carController.InvokeRepeating("Brakes", 0f, 0.1f);
			}
		}
            
	}
}