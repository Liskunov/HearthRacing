using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Cars
{
	public class CarAI : MonoBehaviour
	{
		private AdvancedCarController m_carController;
		[Range(0,1)]
		[SerializeField] private int goPush;

		[SerializeField] List<GameObject> targetPoints = new List<GameObject>();
		private int CountTarget;
		private Vector3 Target;
		private Vector3 NextTarget;
		private int I;
		private void Awake()
		{
			m_carController = GetComponent<AdvancedCarController>();
			CountTarget = targetPoints.Count;
		}

		private void Start()
		{
			I = 0;
			Target = targetPoints[I].transform.position;
			//NextTarget = targetPoints[I++].transform.position;
			Debug.Log(targetPoints.Count);
		}

		private void Update()
		{
			Debug.Log(I);
			Vector3 DirPoint = (Target - transform.position).normalized;
			float dot = Vector3.Dot(transform.forward, DirPoint);
			float disToPos = Vector3.Distance(transform.position, Target);
			float angleDir = Vector3.SignedAngle(transform.forward, DirPoint, Vector3.up);

			if (disToPos > 5f)
			{
				m_carController.GoForward();
			}
			else
			{
				SwapTarget();
			}
			
			if (angleDir > 0f)
			{
				m_carController.TurnRight();
			}
			else //if (angleDir < 0f)
			{
				m_carController.TurnLeft();
			}
			m_carController.AnimateWheelMeshes();
		}

		private void SwapTarget()
		{
			if (I == CountTarget)
			{
				m_carController.InvokeRepeating("Brakes", 0f, 0.1f);
			}
			else
			{
				I++;
				Target = targetPoints[I].transform.position;
				//NextTarget = targetPoints[I++].transform.position;
			}
		}
		private void DecelCar()
		{
			m_carController.InvokeRepeating("DecelerateCar", 0f, 0.1f);
		}
	}

}