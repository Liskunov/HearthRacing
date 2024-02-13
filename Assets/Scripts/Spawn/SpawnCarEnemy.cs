using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class SpawnCarEnemy : MonoBehaviour
{
    public void SpawnEnemyCar()
    {
        for (int i = 0; i < StaticInfo.spawnPointsEnemy.Count; i++)
        {
            var allCars = Resources.LoadAll("Cars/CarTier" + StaticInfo.lvlTav);
            var index = Random.Range(0, allCars.Length);
            var car = Instantiate(allCars[index], StaticInfo.spawnPointsEnemy[i].transform);
        }
    }
}
