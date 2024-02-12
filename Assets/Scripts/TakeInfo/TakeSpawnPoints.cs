using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeSpawnPoints : MonoBehaviour
{
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            StaticInfo.spawnPoints[i] = transform.GetChild(i).gameObject;
        }
    }
}
