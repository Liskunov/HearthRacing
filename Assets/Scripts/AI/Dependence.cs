using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dependence : MonoBehaviour
{
	
	public static float SetDistansToStop(int maxSpeed , float betweenTargets, float disToStop)
	{
		if (betweenTargets > 60)
		{
			if (maxSpeed >= 180)
			{
				disToStop = 90f;
				return disToStop;
			}

			if (maxSpeed >= 170)
			{
				disToStop = 90f;
				return disToStop;
			}

			if (maxSpeed >= 160)
			{
				disToStop = 90f;
				return disToStop;
			}

			if (maxSpeed >= 150)
			{
				disToStop = 90f;
				return disToStop;
			}

			if (maxSpeed >= 140)
			{
				disToStop = 90f;
				return disToStop;
			}

			if (maxSpeed >= 130)
			{
				disToStop = 90f;
				return disToStop;
			}

			if (maxSpeed >= 120)
			{
				disToStop = 90f;
				return disToStop;
			}

			if (maxSpeed >= 110)
			{
				disToStop = 90f;
				return disToStop;
			}

			if (maxSpeed >= 100)
			{
				disToStop = 90f;
				return disToStop;
			}

			if (maxSpeed >= 90)
			{
				disToStop = 90f;
				return disToStop;
			}

			if (maxSpeed >= 80)
			{
				disToStop = 90f;
				return disToStop;
			}

			if (maxSpeed >= 70)
			{
				disToStop = 30f;
				return disToStop;
			}

			if (maxSpeed >= 60)
			{
				disToStop = 25f;
				return disToStop;
			}
			if (maxSpeed > 49)
			{
				disToStop = 20f;
				return disToStop;
			}
		}
		return disToStop;
	}
	public static float SetDistansToAngle(int maxSpeed , float betweenTargets, float disToTurn)
	{
		if (betweenTargets > 60)
		{
			if (maxSpeed >= 180)
			{
				disToTurn = 90f;
				return disToTurn;
			}

			if (maxSpeed >= 170)
			{
				disToTurn = 90f;
				return disToTurn;
			}

			if (maxSpeed >= 160)
			{
				disToTurn = 90f;
				return disToTurn;
			}

			if (maxSpeed >= 150)
			{
				disToTurn = 90f;
				return disToTurn;
			}

			if (maxSpeed >= 140)
			{
				disToTurn = 90f;
				return disToTurn;
			}

			if (maxSpeed >= 130)
			{
				disToTurn = 90f;
				return disToTurn;
			}

			if (maxSpeed >= 120)
			{
				disToTurn = 90f;
				return disToTurn;
			}

			if (maxSpeed >= 110)
			{
				disToTurn = 90f;
				return disToTurn;
			}

			if (maxSpeed >= 100)
			{
				disToTurn = 90f;
				return disToTurn;
			}

			if (maxSpeed >= 90)
			{
				disToTurn = 90f;
				return disToTurn;
			}

			if (maxSpeed >= 80)
			{
				disToTurn = 90f;
				return disToTurn;
			}

			if (maxSpeed >= 70)
			{
				disToTurn = 20f;
				return disToTurn;
			}

			if (maxSpeed >= 60)
			{
				disToTurn = 17f;
				return disToTurn;
			}
			if (maxSpeed > 49)
			{
				disToTurn = 12f;
				return disToTurn;
			}
		}
		return disToTurn;
	}
}
