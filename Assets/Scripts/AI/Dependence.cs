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
				disToStop = 20f;
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
				disToStop = 60f;
				return disToStop;
			}

			if (maxSpeed >= 70)
			{
				disToStop = 35f;
				return disToStop;
			}
			
			if (maxSpeed >= 65)
			{
				disToStop = 25f;
				return disToStop;
			}

			if (maxSpeed >= 60)
			{
				disToStop = 20f;
				return disToStop;
			}
			if (maxSpeed > 49)
			{
				disToStop = 15f;
				return disToStop;
			}
		}
		else if (betweenTargets <= 60)
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
				disToStop = 12f;
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
				disToStop = 25f;
				return disToStop;
			}

			if (maxSpeed >= 70)
			{
				disToStop = 25f;
				return disToStop;
			}
			
			if (maxSpeed >= 65)
			{
				disToStop = 22f;
				return disToStop;
			}

			if (maxSpeed >= 60)
			{
				disToStop = 20f;
				return disToStop;
			}
			if (maxSpeed > 49)
			{
				disToStop = 15f;
				return disToStop;
			}
		}
		return disToStop;
	}
	public static float SetDistansToAngle(int maxSpeed , float betweenTargets, float disToTurn)
	{
		if (betweenTargets >= 60)
		{
			if (maxSpeed >= 180)
			{
				disToTurn = 17f;
				return disToTurn;
			}

			if (maxSpeed >= 170)
			{
				disToTurn = 17f;
				return disToTurn;
			}

			if (maxSpeed >= 160)
			{
				disToTurn = 17f;
				return disToTurn;
			}

			if (maxSpeed >= 150)
			{
				disToTurn = 17f;
				return disToTurn;
			}

			if (maxSpeed >= 140)
			{
				disToTurn = 17f;
				return disToTurn;
			}

			if (maxSpeed >= 130)
			{
				disToTurn = 17f;
				return disToTurn;
			}

			if (maxSpeed >= 120)
			{
				disToTurn = 17f;
				return disToTurn;
			}

			if (maxSpeed >= 110)
			{
				disToTurn = 17f;
				return disToTurn;
			}

			if (maxSpeed >= 100)
			{
				disToTurn = 17f;
				return disToTurn;
			}

			if (maxSpeed >= 90)
			{
				disToTurn = 17f;
				return disToTurn;
			}

			if (maxSpeed >= 80)
			{
				disToTurn = 17f;
				return disToTurn;
			}

			if (maxSpeed >= 70)
			{
				disToTurn = 17f;
				return disToTurn;
			}

			if (maxSpeed >= 60)
			{
				disToTurn = 15f;
				return disToTurn;
			}
			if (maxSpeed > 49)
			{
				disToTurn = 10f;
				return disToTurn;
			}
		}
		else if (betweenTargets < 60)
		{
			if (maxSpeed >= 180)
			{
				disToTurn = 20f;
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
				disToTurn = 10f;
				return disToTurn;
			}

			if (maxSpeed >= 90)
			{
				disToTurn = 90f;
				return disToTurn;
			}

			if (maxSpeed >= 80)
			{
				disToTurn = 15f;
				return disToTurn;
			}

			if (maxSpeed >= 70)
			{
				disToTurn = 15f;
				return disToTurn;
			}

			if (maxSpeed >= 60)
			{
				disToTurn = 15f;
				return disToTurn;
			}
			if (maxSpeed > 49)
			{
				disToTurn = 12f;
				return disToTurn;
			}
		}
		return 13;
	}

	public static float SetMultiplayerFromAcceleration(float turbo)
	{
		if (turbo > 10)
		{
			turbo = 1.5f;
			return turbo;
		}
		if (turbo > 9)
		{
			turbo = 1.5f;
			return turbo;
		}
		if (turbo > 8)
		{
			turbo = 1.2f;
			return turbo;
		}
		if (turbo > 7)
		{
			turbo = 1.15f;
			return turbo;
		}
		if (turbo > 6)
		{
			turbo = 1.1f;
			return turbo;
		}
		if (turbo > 5)
		{
			turbo = 1.05f;
			return turbo;
		}
		if (turbo > 4)
		{
			turbo = 1f;
			return turbo;
		}

		return 1f;
	}
}
