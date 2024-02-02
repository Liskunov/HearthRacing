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
		private int CountTarget;
		public Vector3 Target;
		private Vector3 NextTarget;
		public int I;
		[SerializeField] private float stopDist;
		[SerializeField] private float angleDist;
		private void Awake()
		{
			var points = GameObject.Find("Points").transform;
			for (int i = 0; i < points.childCount; i++)
			{
				targetPoints[i] = points.GetChild(i);
			}
			m_carController = GetComponent<AdvancedCarController>();
			CountTarget = targetPoints.Count;
		}

		private void Start()
		{
			I = 0;
			Target = targetPoints[I].position;
			//NextTarget = targetPoints[I++].transform.position;
		}

		private void Update()
		{
			Vector3 DirPoint = (Target - transform.position).normalized;
			float disToPos = Vector3.Distance(transform.position, Target);
			

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
				Target = targetPoints[I].position;
				//NextTarget = targetPoints[I++].transform.position;
			}
		}
		private void DecelCar()
		{
			m_carController.InvokeRepeating("DecelerateCar", 0f, 0.1f);
		}
		
	}
}