using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars
{
	public class CarAI : MonoBehaviour
	{
		private AdvancedCarController m_carController;
		[SerializeField] private Pid_Controller m_pidController;

		[SerializeField] List<GameObject> targetPoints = new List<GameObject>();
		private int CountTarget;
		private Vector3 Target;
		private Vector3 NextTarget;
		private int I;
		[SerializeField] private float stopDist;
		private float stopDistDiv2;
		[SerializeField] private float angleDist;
		private float angleDistDiv2;
		private void Awake()
		{
			m_carController = GetComponent<AdvancedCarController>();
			CountTarget = targetPoints.Count;
			stopDistDiv2 = stopDist / 2;
			angleDistDiv2 = angleDist / 1.5f;
		}

		private void Start()
		{
			I = 0;
			Target = targetPoints[I].transform.position;
			NextTarget = targetPoints[I+1].transform.position;
		}

		private void Update()
		{
			Vector3 DirPoint = (Target - transform.position).normalized;
			//float dot = Vector3.Dot(transform.forward, DirPoint);
			float disToPos = Vector3.Distance(transform.position, Target);
			//float angleDir = Vector3.SignedAngle(transform.forward, DirPoint, Vector3.up);
			float disCurrDisNext = Vector3.Distance(Target, NextTarget);
			Debug.Log(Target);
			Debug.Log(NextTarget);
			Debug.Log(disCurrDisNext);

			if (disCurrDisNext > 30f)
			{
				if (disToPos > stopDist)
				{
					m_carController.GoForward();
				}
				else if (stopDist > disToPos && disToPos > angleDist)
				{
					m_carController.Brakes();
				}
				else
				{
					SwapTarget();
				}
			}
			else
			{
				if (disToPos > stopDistDiv2)
				{
					m_carController.GoForward();
				}
				else if (stopDistDiv2 > disToPos && disToPos > angleDistDiv2)
				{
					//m_carController.Handbrake();
				}
				else
				{
					SwapTarget();
				}
			}
			m_carController.AnimateWheelMeshes();
		}
		void FixedUpdate()
		{
			var targetPosition = Target;
			targetPosition.y = transform.position.y;
			var targetDir = (targetPosition - transform.position).normalized;
			var forwardDir = transform.rotation * Vector3.forward;

			var currentAngle = Vector3.SignedAngle(Vector3.forward, forwardDir, Vector3.up);
			var targetAngle = Vector3.SignedAngle(Vector3.forward, targetDir, Vector3.up);

			float input = m_pidController.UpdateAngle(Time.fixedDeltaTime, currentAngle, targetAngle);
				m_carController.TurnSide(input);
        }

            public void SwapTarget()
		{
			if (I == CountTarget)
			{
				m_carController.InvokeRepeating("Brakes", 0f, 0.1f);
			}
			else
			{
				I++;
				Target = targetPoints[I].transform.position;
				NextTarget = targetPoints[I+1].transform.position;
			}
		}
		private void DecelCar()
		{
			m_carController.InvokeRepeating("DecelerateCar", 0f, 0.1f);
		}
		
	}
}