using System;
using Unity.Mathematics;
using UnityEngine;

namespace CarController.AdvancedCarController
{
	public class AdvancedCarController : MonoBehaviour
	{ 
		[Range(20, 190)]
		[SerializeField] private int m_maxSpeed = 90; 
		[Range(10, 120)]
		[SerializeField] private int m_maxReverseSpeed = 45; 
		[Range(1, 10)]
		[SerializeField] private int m_accelerationMultiplier = 2; 
		[Range(10, 45)]
		[SerializeField] private int m_maxSteeringAngle = 30; 
		[Range(0.1f, 1f)]
		[SerializeField] private float m_steeringSpeed = 0.5f; 
		[Range(100, 600)]
		[SerializeField] private int m_brakeForce = 350; 
		[Range(1, 10)]
		[SerializeField] private int m_decelerationMultiplier = 2; 
		[Range(1, 10)]
		[SerializeField] private int m_handbrakeDriftMultiplier = 5; 
		[SerializeField] private Vector3 m_bodyMassCenter; 
		
		[SerializeField] private GameObject m_frontLeftMesh;
		[SerializeField] private WheelCollider m_frontLeftCollider;
		[SerializeField] private GameObject m_frontRightMesh;
		[SerializeField] private WheelCollider m_frontRightCollider;
		[SerializeField] private GameObject m_rearLeftMesh;
		[SerializeField] private WheelCollider m_rearLeftCollider;
		[SerializeField] private GameObject m_rearRightMesh;
		[SerializeField] private WheelCollider m_rearRightCollider;
		
		[SerializeField] private bool m_useEffects;
		
		[SerializeField] private ParticleSystem m_RLWParticleSystem;
		[SerializeField] private ParticleSystem m_RRWParticleSystem;
		
		[SerializeField] private TrailRenderer m_RLWTireSkid;
		[SerializeField] private TrailRenderer m_RRWTireSkid;

		private float m_carSpeed; 
		private bool m_isDrifting; 
		private bool m_isTractionLocked; 
		
		private Rigidbody m_carRigidbody; 
		private float m_steeringAxis; 
		private float m_throttleAxis; 
		private float m_driftingAxis;
		private float m_localVelocityZ;
		private float m_localVelocityX;
		private bool m_deceleratingCar;

		private WheelFrictionCurve m_FLwheelFriction;
		private float m_FLWextremumSlip;
		private WheelFrictionCurve m_FRwheelFriction;
		private float m_FRWextremumSlip;
		private WheelFrictionCurve m_RLwheelFriction;
		private float m_RLWextremumSlip;
		private WheelFrictionCurve m_RRwheelFriction;
		private float m_RRWextremumSlip;
		
		void Start()
		{
			m_carRigidbody = gameObject.GetComponent<Rigidbody>();
			m_carRigidbody.centerOfMass = m_bodyMassCenter;

			m_FLwheelFriction = SetupFriction(m_frontLeftCollider, out m_FLWextremumSlip);
			m_FRwheelFriction = SetupFriction(m_frontRightCollider, out m_FRWextremumSlip);
			m_RLwheelFriction = SetupFriction(m_rearLeftCollider, out m_RLWextremumSlip);
			m_RRwheelFriction = SetupFriction(m_rearRightCollider, out m_RRWextremumSlip);
			
			if(!m_useEffects)
			{
				if(m_RLWParticleSystem != null)
				{
					m_RLWParticleSystem.Stop();
				}
				if(m_RRWParticleSystem != null)
				{
					m_RRWParticleSystem.Stop();
				}
				if(m_RLWTireSkid != null)
				{
					m_RLWTireSkid.emitting = false;
				}
				if(m_RRWTireSkid != null)
				{
					m_RRWTireSkid.emitting = false;
				}
			}
		}

		void Update()
		{
			m_carSpeed = (2 * math.PI * m_frontLeftCollider.radius * m_frontLeftCollider.rpm * 60) / 1000;
			var transformDirection = transform.InverseTransformDirection(m_carRigidbody.velocity);
			m_localVelocityX = transformDirection.x;
			m_localVelocityZ = transformDirection.z;
			
			if(Input.GetKey(KeyCode.W))
			{
				CancelInvoke("DecelerateCar");
				m_deceleratingCar = false;
				GoForward();
			}
			if(Input.GetKey(KeyCode.S))
			{
				CancelInvoke("DecelerateCar");
				m_deceleratingCar = false;
				GoReverse();
			}
			if(Input.GetKey(KeyCode.A))
			{
				TurnLeft();
			}
			if(Input.GetKey(KeyCode.D))
			{
				TurnRight();
			}
			if(Input.GetKey(KeyCode.Space))
			{
				CancelInvoke("DecelerateCar");
				m_deceleratingCar = false;
				Handbrake();
			}
			if(Input.GetKeyUp(KeyCode.Space))
			{
				RecoverTraction();
			}
			if((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)))
			{
				ThrottleOff();
			}
			if((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) && !Input.GetKey(KeyCode.Space) && !m_deceleratingCar)
			{
				InvokeRepeating("DecelerateCar", 0f, 0.1f);
				m_deceleratingCar = true;
			}
			if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && m_steeringAxis != 0f)
			{
				ResetSteeringAngle();
			}
			
			AnimateWheelMeshes();
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
			
		private void TurnLeft()
		{
			TurnSide(-1);
		}

		private void TurnRight()
		{
			TurnSide(1);
		}
		
		private void TurnSide(int multiplier)
		{
			m_steeringAxis += Time.deltaTime * 10f * m_steeringSpeed * multiplier;
			if(math.abs(m_steeringAxis) > 1f)
			{
				m_steeringAxis = 1f * multiplier;
			}
			
			var steeringAngle = m_steeringAxis * m_maxSteeringAngle;
			m_frontLeftCollider.steerAngle = math.lerp(m_frontLeftCollider.steerAngle, steeringAngle, m_steeringSpeed);
			m_frontRightCollider.steerAngle = math.lerp(m_frontRightCollider.steerAngle, steeringAngle, m_steeringSpeed);
		}

		private void ResetSteeringAngle()
		{
			if(m_steeringAxis < 0f)
			{
				m_steeringAxis += (Time.deltaTime * 10f * m_steeringSpeed);
			}
			else if(m_steeringAxis > 0f)
			{
				m_steeringAxis -= (Time.deltaTime * 10f * m_steeringSpeed);
			}
			if(math.abs(m_frontLeftCollider.steerAngle) < 1f)
			{
				m_steeringAxis = 0f;
			}
			
			var steeringAngle = m_steeringAxis * m_maxSteeringAngle;
			m_frontLeftCollider.steerAngle = math.lerp(m_frontLeftCollider.steerAngle, steeringAngle, m_steeringSpeed);
			m_frontRightCollider.steerAngle = math.lerp(m_frontRightCollider.steerAngle, steeringAngle, m_steeringSpeed);
		}

		private void AnimateWheelMeshes()
		{
			Vector3 position;
			Quaternion rotation;
			
			m_frontLeftCollider.GetWorldPose(out position, out rotation);
			m_frontLeftMesh.transform.SetPositionAndRotation(position, rotation);

			m_frontRightCollider.GetWorldPose(out position, out rotation);
			m_frontRightMesh.transform.SetPositionAndRotation(position, rotation);

			m_rearLeftCollider.GetWorldPose(out position, out rotation);
			m_rearLeftMesh.transform.SetPositionAndRotation(position, rotation);

			m_rearRightCollider.GetWorldPose(out position, out rotation);
			m_rearRightMesh.transform.SetPositionAndRotation(position, rotation);
		}
		
		private void GoForward()
		{
			if(math.abs(m_localVelocityX) > 2.5f)
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
			if(m_throttleAxis > 1f)
			{
				m_throttleAxis = 1f;
			}
			if(m_localVelocityZ < -1f)
			{
				Brakes();
			}
			else
			{
				if(Mathf.RoundToInt(m_carSpeed) < m_maxSpeed)
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
			if(math.abs(m_localVelocityX) > 2.5f)
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
			if(m_throttleAxis < -1f)
			{
				m_throttleAxis = -1f;
			}
			if(m_localVelocityZ > 1f)
			{
				Brakes();
			}
			else
			{
				if(math.abs(Mathf.RoundToInt(m_carSpeed)) < m_maxReverseSpeed)
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
			m_frontLeftCollider.brakeTorque = 0;
			m_frontLeftCollider.motorTorque = (m_accelerationMultiplier * 50f) * m_throttleAxis;
			m_frontRightCollider.brakeTorque = 0;
			m_frontRightCollider.motorTorque = (m_accelerationMultiplier * 50f) * m_throttleAxis;
			m_rearLeftCollider.brakeTorque = 0;
			m_rearLeftCollider.motorTorque = (m_accelerationMultiplier * 50f) * m_throttleAxis;
			m_rearRightCollider.brakeTorque = 0;
			m_rearRightCollider.motorTorque = (m_accelerationMultiplier * 50f) * m_throttleAxis;
		}
		
		private void ThrottleOff()
		{
			m_frontLeftCollider.motorTorque = 0;
			m_frontRightCollider.motorTorque = 0;
			m_rearLeftCollider.motorTorque = 0;
			m_rearRightCollider.motorTorque = 0;
		}

		private void DecelerateCar()
		{
			if(math.abs(m_localVelocityX) > 2.5f)
			{
				m_isDrifting = true;
				DriftCarPS();
			}
			else
			{
				m_isDrifting = false;
				DriftCarPS();
			}
			if(m_throttleAxis != 0f)
			{
				if(m_throttleAxis > 0f)
				{
					m_throttleAxis -= (Time.deltaTime * 10f);
				}
				else if(m_throttleAxis < 0f)
				{
					m_throttleAxis += (Time.deltaTime * 10f);
				}
				if(math.abs(m_throttleAxis) < 0.15f)
				{
					m_throttleAxis = 0f;
				}
			}
			m_carRigidbody.velocity *= (1f / (1f + (0.025f * m_decelerationMultiplier)));
			ThrottleOff();
			
			if(m_carRigidbody.velocity.magnitude < 0.25f)
			{
				m_carRigidbody.velocity = Vector3.zero;
				CancelInvoke("DecelerateCar");
			}
		}

		private void Brakes()
		{
			m_frontLeftCollider.brakeTorque = m_brakeForce;
			m_frontRightCollider.brakeTorque = m_brakeForce;
			m_rearLeftCollider.brakeTorque = m_brakeForce;
			m_rearRightCollider.brakeTorque = m_brakeForce;
		}
	
		private void Handbrake()
		{
			CancelInvoke("RecoverTraction");
			
			m_driftingAxis += (Time.deltaTime);
			float secureStartingPoint = m_driftingAxis * m_FLWextremumSlip * m_handbrakeDriftMultiplier;

			if(secureStartingPoint < m_FLWextremumSlip)
			{
				m_driftingAxis = m_FLWextremumSlip / (m_FLWextremumSlip * m_handbrakeDriftMultiplier);
			}
			if(m_driftingAxis > 1f)
			{
				m_driftingAxis = 1f;
			}
			
			if(math.abs(m_localVelocityX) > 2.5f)
			{
				m_isDrifting = true;
			}
			else
			{
				m_isDrifting = false;
			}
			
			if(m_driftingAxis < 1f)
			{
				m_FLwheelFriction.extremumSlip = m_FLWextremumSlip * m_handbrakeDriftMultiplier * m_driftingAxis;
				m_frontLeftCollider.sidewaysFriction = m_FLwheelFriction;

				m_FRwheelFriction.extremumSlip = m_FRWextremumSlip * m_handbrakeDriftMultiplier * m_driftingAxis;
				m_frontRightCollider.sidewaysFriction = m_FRwheelFriction;

				m_RLwheelFriction.extremumSlip = m_RLWextremumSlip * m_handbrakeDriftMultiplier * m_driftingAxis;
				m_rearLeftCollider.sidewaysFriction = m_RLwheelFriction;

				m_RRwheelFriction.extremumSlip = m_RRWextremumSlip * m_handbrakeDriftMultiplier * m_driftingAxis;
				m_rearRightCollider.sidewaysFriction = m_RRwheelFriction;
			}

			m_isTractionLocked = true;
			DriftCarPS();
		}
		
		private void DriftCarPS()
		{
			if(m_useEffects)
			{
				if(m_isDrifting)
				{
					m_RLWParticleSystem.Play();
					m_RRWParticleSystem.Play();
				}
				else if(!m_isDrifting)
				{
					m_RLWParticleSystem.Stop();
					m_RRWParticleSystem.Stop();
				} 
				if((m_isTractionLocked || math.abs(m_localVelocityX) > 5f) && math.abs(m_carSpeed) > 12f)
				{
					m_RLWTireSkid.emitting = true;
					m_RRWTireSkid.emitting = true;
				}
				else 
				{
					m_RLWTireSkid.emitting = false;
					m_RRWTireSkid.emitting = false;
				}
			}
			else if(!m_useEffects)
			{
				if(m_RLWParticleSystem != null)
				{
					m_RLWParticleSystem.Stop();
				}
				if(m_RRWParticleSystem != null)
				{
					m_RRWParticleSystem.Stop();
				}
				if(m_RLWTireSkid != null)
				{
					m_RLWTireSkid.emitting = false;
				}
				if(m_RRWTireSkid != null)
				{
					m_RRWTireSkid.emitting = false;
				}
			}
		}

		private void RecoverTraction()
		{
			m_isTractionLocked = false;
			m_driftingAxis -= (Time.deltaTime / 1.5f);
			if(m_driftingAxis < 0f)
			{
				m_driftingAxis = 0f;
			}
			
			if(m_FLwheelFriction.extremumSlip > m_FLWextremumSlip)
			{
				m_FLwheelFriction.extremumSlip = m_FLWextremumSlip * m_handbrakeDriftMultiplier * m_driftingAxis;
				m_frontLeftCollider.sidewaysFriction = m_FLwheelFriction;

				m_FRwheelFriction.extremumSlip = m_FRWextremumSlip * m_handbrakeDriftMultiplier * m_driftingAxis;
				m_frontRightCollider.sidewaysFriction = m_FRwheelFriction;

				m_RLwheelFriction.extremumSlip = m_RLWextremumSlip * m_handbrakeDriftMultiplier * m_driftingAxis;
				m_rearLeftCollider.sidewaysFriction = m_RLwheelFriction;

				m_RRwheelFriction.extremumSlip = m_RRWextremumSlip * m_handbrakeDriftMultiplier * m_driftingAxis;
				m_rearRightCollider.sidewaysFriction = m_RRwheelFriction;

				Invoke("RecoverTraction", Time.deltaTime);
			}
			else if (m_FLwheelFriction.extremumSlip < m_FLWextremumSlip)
			{
				m_FLwheelFriction.extremumSlip = m_FLWextremumSlip;
				m_frontLeftCollider.sidewaysFriction = m_FLwheelFriction;

				m_FRwheelFriction.extremumSlip = m_FRWextremumSlip;
				m_frontRightCollider.sidewaysFriction = m_FRwheelFriction;

				m_RLwheelFriction.extremumSlip = m_RLWextremumSlip;
				m_rearLeftCollider.sidewaysFriction = m_RLwheelFriction;

				m_RRwheelFriction.extremumSlip = m_RRWextremumSlip;
				m_rearRightCollider.sidewaysFriction = m_RRwheelFriction;

				m_driftingAxis = 0f;
			}
		}
	}
}
