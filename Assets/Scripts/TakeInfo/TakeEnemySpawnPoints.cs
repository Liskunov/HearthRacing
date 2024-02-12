using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeEnemySpawnPoints : MonoBehaviour
{
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            StaticInfo.spawnPointsEnemy[i] = transform.GetChild(i).gameObject;
        }
    }
}
