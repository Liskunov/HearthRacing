using System;
using Unity.Mathematics;
using UnityEngine;

namespace Cars
{
	public class AdvancedCarController : MonoBehaviour
	{
		[Range(20, 190)] [SerializeField] public int m_maxSpeed = 90;
		[Range(10, 120)]  private int m_maxReverseSpeed = 45;
		[Range(1, 12)] [SerializeField] public int m_accelerationMultiplier = 2;
		[Range(10, 45)]  private int m_maxSteeringAngle = 35;
		[Range(0.1f, 1f)]  private float m_steeringSpeed = 1f;
		[Range(100, 600)] [SerializeField] public int m_brakeForce = 350;
		[Range(1, 10)]  private int m_decelerationMultiplier = 2;
		[Range(1, 10)]  private int m_handbrakeDriftMultiplier = 5;
		[SerializeField] private Vector3 m_bodyMassCenter;

		[SerializeField] private GameObject[] m_tireMeshes;
		[SerializeField] private GameObject[] m_rimMeshes;
		[SerializeField] private WheelCollider[] m_wheelColliders;

		private bool m_useEffects;

		private ParticleSystem[] m_particleSystems;
		private TrailRenderer[] m_tireSkids;

		private int m_wheelsCount;
		private int m_particlesCount;
		private int m_tireSkidsCount;
		public float m_carSpeed;
		public float carSpeedInAI;
		private bool m_isDrifting;
		private bool m_isTractionLocked;

		private Rigidbody m_carRigidbody;
		private float m_steeringAxis;
		private float m_throttleAxis;
		private float m_driftingAxis;
		private float m_localVelocityZ;
		private float m_localVelocityX;
		private bool m_deceleratingCar;

		private WheelFrictionCurve[] m_wheelFrictionCurves;
		private float[] m_extremumSlips;

		void Start()
		{
			m_wheelsCount = m_tireMeshes.Length;
			//m_particlesCount = m_particleSystems.Length;
			//m_tireSkidsCount = m_tireSkids.Length;
			m_extremumSlips = new float[m_wheelsCount];
			m_wheelFrictionCurves = new WheelFrictionCurve[m_wheelsCount];

			m_carRigidbody = gameObject.GetComponent<Rigidbody>();
			m_carRigidbody.centerOfMass = m_bodyMassCenter;


			for (int i = 0; i < m_wheelsCount; i++)
			{
				m_wheelFrictionCurves[i] = SetupFriction(m_wheelColliders[i], out m_extremumSlips[i]);
			}

			if (!m_useEffects)
			{
				for (int i = 0; i < m_particlesCount; i++)
				{
					if (m_particleSystems[i] != null)
					{
						m_particleSystems[i].Stop();
					}
				}

				for (int i = 0; i < m_tireSkidsCount; i++)
				{
					if (m_tireSkids[i] != null)
					{
						m_tireSkids[i].emitting = false;
					}
				}
			}
		}

		void Update()
		{
			m_carSpeed = (2 * math.PI * m_wheelColliders[0].radius * m_wheelColliders[0].rpm * 60) / 1000;
			var transformDirection = transform.InverseTransformDirection(m_carRigidbody.velocity);
			m_localVelocityX = transformDirection.x;
			m_localVelocityZ = transformDirection.z;

			// if(Input.GetKey(KeyCode.W))
			// {
			// 	CancelInvoke("DecelerateCar");
			// 	m_deceleratingCar = false;
			// 	GoForward();
			// }
			// if(Input.GetKey(KeyCode.S))
			// {
			// 	CancelInvoke("DecelerateCar");
			// 	m_deceleratingCar = false;
			// 	GoReverse();
			// }
			// if(Input.GetKey(KeyCode.A))
			// {
			// 	TurnLeft();
			// }
			// if(Input.GetKey(KeyCode.D))
			// {
			// 	TurnRight();
			// }
			// if(Input.GetKey(KeyCode.Space))
			// {
			// 	CancelInvoke("DecelerateCar");
			// 	m_deceleratingCar = false;
			// 	Handbrake();
			// }
			// if(Input.GetKeyUp(KeyCode.Space))
			// {
			// 	RecoverTraction();
			// }
			// if((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)))
			// {
			// 	ThrottleOff();
			// }
			// if((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) && !Input.GetKey(KeyCode.Space) && !m_deceleratingCar)
			// {
			// 	InvokeRepeating("DecelerateCar", 0f, 0.1f);
			// 	m_deceleratingCar = true;
			// }
			// if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && m_steeringAxis != 0f)
			// {
			// 	ResetSteeringAngle();
			// }
			//
			// AnimateWheelMeshes();
		}

		private void FixedUpdate()
		{
			carSpeedInAI = m_carRigidbody.velocity.magnitude;
		}

		private WheelFrictionCurve SetupFriction(WheelCollider wheelCollider, out float extremumSlip)
		{
			var sidewaysFriction = wheelCollider.sidewaysFriction;
			WheelFrictionCurve wheelFriction = new WheelFrictionCurve
			{
				extremumSlip = sidewaysFriction.extremumSlip,
				extremumValue = sidewaysFriction.extremumValue,
				asymptoteSlip = sidewaysFriction.asymptoteSlip,
				asymptoteValue = sidewaysFriction.asymptoteValue,
				stiffness = sidewaysFriction.stiffness
			};
			extremumSlip = sidewaysFriction.extremumSlip;

			return wheelFriction;
		}

		public void TurnLeft()
		{
			TurnSide(-1);
		}

		public void TurnRight()
		{
			TurnSide(1);
		}

		public void TurnSide(float multiplier)
		{
			m_steeringAxis += Time.deltaTime * 10f * m_steeringSpeed * multiplier;
			if (math.abs(m_steeringAxis) > 1f)
			{
				m_steeringAxis = 1f * multiplier;
			}

			var steeringAngle = m_steeringAxis * m_maxSteeringAngle;
			m_wheelColliders[0].steerAngle =
				Mathf.MoveTowards(m_wheelColliders[0].steerAngle, steeringAngle, m_steeringSpeed);
			m_wheelColliders[1].steerAngle =
				Mathf.MoveTowards(m_wheelColliders[1].steerAngle, steeringAngle, m_steeringSpeed);
		}
		
		public void TurnSideAI(float steeringAngle)
		{
			m_steeringAxis += Time.deltaTime * 10f * m_steeringSpeed;
			

			//var steeringAngle = m_steeringAxis * m_maxSteeringAngle;
			m_wheelColliders[0].steerAngle =
				Mathf.MoveTowards(m_wheelColliders[0].steerAngle, steeringAngle, m_steeringSpeed);
			m_wheelColliders[1].steerAngle =
				Mathf.MoveTowards(m_wheelColliders[1].steerAngle, steeringAngle, m_steeringSpeed);
		}

		private void ResetSteeringAngle()
		{
			if (m_steeringAxis < 0f)
			{
				m_steeringAxis += (Time.deltaTime * 10f * m_steeringSpeed);
			}
			else if (m_steeringAxis > 0f)
			{
				m_steeringAxis -= (Time.deltaTime * 10f * m_steeringSpeed);
			}

			if (math.abs(m_wheelColliders[0].steerAngle) < 1f)
			{
				m_steeringAxis = 0f;
			}

			var steeringAngle = m_steeringAxis * m_maxSteeringAngle;
			m_wheelColliders[0].steerAngle = math.lerp(m_wheelColliders[0].steerAngle, steeringAngle, m_steeringSpeed);
			m_wheelColliders[1].steerAngle = math.lerp(m_wheelColliders[1].steerAngle, steeringAngle, m_steeringSpeed);
		}

		public void AnimateWheelMeshes()
		{
			Vector3 position;
			Quaternion rotation;

			for (int i = 0; i < m_wheelsCount; i++)
			{
				m_wheelColliders[i].GetWorldPose(out position, out rotation);
				m_tireMeshes[i].transform.SetPositionAndRotation(position, rotation);
				m_rimMeshes[i].transform.SetPositionAndRotation(position, rotation);
			}
		}

		public void GoForward()
		{
			if (math.abs(m_localVelocityX) > 2.5f)
			{
				m_isDrifting = true;
				DriftCarPS();
			}
			else
			{
				m_isDrifting = false;
				DriftCarPS();
			}

			m_throttleAxis += (Time.deltaTime * 3f);
			if (m_throttleAxis > 1f)
			{
				m_throttleAxis = 1f;
			}

			if (m_localVelocityZ < -1f)
			{
				Brakes();
			}
			else
			{
				if (Mathf.RoundToInt(m_carSpeed) < m_maxSpeed)
				{
					ThrottleOn();
				}
				else
				{
					ThrottleOff();
				}
			}
		}

		private void GoReverse()
		{
			if (math.abs(m_localVelocityX) > 2.5f)
			{
				m_isDrifting = true;
				DriftCarPS();
			}
			else
			{
				m_isDrifting = false;
				DriftCarPS();
			}

			m_throttleAxis -= (Time.deltaTime * 3f);
			if (m_throttleAxis < -1f)
			{
				m_throttleAxis = -1f;
			}

			if (m_localVelocityZ > 1f)
			{
				Brakes();
			}
			else
			{
				if (math.abs(Mathf.RoundToInt(m_carSpeed)) < m_maxReverseSpeed)
				{
					ThrottleOn();
				}
				else
				{
					ThrottleOff();
				}
			}
		}

		private void ThrottleOn()
		{
			for (int i = 0; i < m_wheelsCount; i++)
			{
				m_wheelColliders[i].brakeTorque = 0;
				m_wheelColliders[i].motorTorque = (m_accelerationMultiplier * 50f) * m_throttleAxis;
			}
		}

		private void ThrottleOff()
		{
			for (int i = 0; i < m_wheelsCount; i++)
			{
				m_wheelColliders[i].motorTorque = 0;
			}
		}

		public void DecelerateCar()
		{
			if (math.abs(m_localVelocityX) > 2.5f)
			{
				m_isDrifting = true;
				DriftCarPS();
			}
			else
			{
				m_isDrifting = false;
				DriftCarPS();
			}

			if (m_throttleAxis != 0f)
			{
				if (m_throttleAxis > 0f)
				{
					m_throttleAxis -= (Time.deltaTime * 10f);
				}
				else if (m_throttleAxis < 0f)
				{
					m_throttleAxis += (Time.deltaTime * 10f);
				}

				if (math.abs(m_throttleAxis) < 0.15f)
				{
					m_throttleAxis = 0f;
				}
			}

			m_carRigidbody.velocity *= (1f / (1f + (0.025f * m_decelerationMultiplier)));
			ThrottleOff();

			if (m_carRigidbody.velocity.magnitude < 0.25f)
			{
				m_carRigidbody.velocity = Vector3.zero;
				CancelInvoke("DecelerateCar");
			}
		}

		public void Brakes()
		{
			for (int i = 0; i < m_wheelsCount; i++)
			{
				m_wheelColliders[i].brakeTorque = m_brakeForce;
			}
		}

		private void Handbrake()
		{
			CancelInvoke("RecoverTraction");

			m_driftingAxis += (Time.deltaTime);
			float secureStartingPoint = m_driftingAxis * m_extremumSlips[0] * m_handbrakeDriftMultiplier;

			if (secureStartingPoint < m_extremumSlips[0])
			{
				m_driftingAxis = m_extremumSlips[0] / (m_extremumSlips[0] * m_handbrakeDriftMultiplier);
			}

			if (m_driftingAxis > 1f)
			{
				m_driftingAxis = 1f;
			}

			if (math.abs(m_localVelocityX) > 2.5f)
			{
				m_isDrifting = true;
			}
			else
			{
				m_isDrifting = false;
			}

			if (m_driftingAxis < 1f)
			{
				for (int i = 0; i < m_wheelsCount; i++)
				{
					m_wheelFrictionCurves[i].extremumSlip =
						m_extremumSlips[i] * m_handbrakeDriftMultiplier * m_driftingAxis;
					m_wheelColliders[i].sidewaysFriction = m_wheelFrictionCurves[i];
				}
			}

			m_isTractionLocked = true;
			DriftCarPS();
		}

		private void DriftCarPS()
		{
			if (m_useEffects)
			{
				if (m_isDrifting)
				{
					for (int i = 0; i < m_particlesCount; i++)
					{
						m_particleSystems[i].Play();
					}
				}
				else if (!m_isDrifting)
				{
					for (int i = 0; i < m_particlesCount; i++)
					{
						m_particleSystems[i].Stop();
					}
				}

				if ((m_isTractionLocked || math.abs(m_localVelocityX) > 5f) && math.abs(m_carSpeed) > 12f)
				{
					for (int i = 0; i < m_tireSkidsCount; i++)
					{
						m_tireSkids[i].emitting = true;
					}
				}
				else
				{
					for (int i = 0; i < m_tireSkidsCount; i++)
					{
						m_tireSkids[i].emitting = false;
					}
				}
			}
			else if (!m_useEffects)
			{
				for (int i = 0; i < m_particlesCount; i++)
				{
					if (m_particleSystems[i] != null)
					{
						m_particleSystems[i].Stop();
					}
				}

				for (int i = 0; i < m_tireSkidsCount; i++)
				{
					if (m_tireSkids[i] != null)
					{
						m_tireSkids[i].emitting = false;
					}
				}
			}
		}

		private void RecoverTraction()
		{
			m_isTractionLocked = false;
			m_driftingAxis -= (Time.deltaTime / 1.5f);
			if (m_driftingAxis < 0f)
			{
				m_driftingAxis = 0f;
			}

			if (m_wheelFrictionCurves[0].extremumSlip > m_extremumSlips[0])
			{
				for (int i = 0; i < m_wheelsCount; i++)
				{
					m_wheelFrictionCurves[i].extremumSlip =
						m_extremumSlips[i] * m_handbrakeDriftMultiplier * m_driftingAxis;
					m_wheelColliders[i].sidewaysFriction = m_wheelFrictionCurves[i];
				}

				Invoke("RecoverTraction", Time.deltaTime);
			}
			else if (m_wheelFrictionCurves[0].extremumSlip < m_extremumSlips[0])
			{
				for (int i = 0; i < m_wheelsCount; i++)
				{
					m_wheelFrictionCurves[i].extremumSlip = m_extremumSlips[i];
					m_wheelColliders[i].sidewaysFriction = m_wheelFrictionCurves[i];
				}

				m_driftingAxis = 0f;
			}
		}

		public int maxSpeed => m_maxSpeed;
	}
}
