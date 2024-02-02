using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    [SerializeField] public RatingManager[] ratingManagers;

    public void SpawnCarInPoint()
    {
        for (int i = 0; i < ratingManagers.Length; i++)
        {
            var spawnPoint = GameObject.Find("SpawnPoint" + i);
            
            if (spawnPoint.transform.childCount != 0)
            {
                Transform transform = spawnPoint.transform.GetChild(0);
                GameObject.Destroy(transform.gameObject);
            } 
            
            if (ratingManagers[i].CarSlot.transform.childCount != 0)
            {
                var nameCar = ratingManagers[i].CarSlot.GetComponentInChildren<CarImgInfo>().carImgSO.nameCar;
                Instantiate(PrefabUtility.LoadPrefabContents("Assets/Prefabs/Cars/Test/" + nameCar + ".prefab"), spawnPoint.transform);
                
                
                for (int j = 0; j < spawnPoint.GetComponentInChildren<CarInfo>().specificationsInt.Count; j++)
                {
                    spawnPoint.GetComponentInChildren<CarInfo>().specificationsInt[j] = ratingManagers[i].CarSlot.GetComponentInChildren<CarImgInfo>().specifications[j];
                    Debug.Log(spawnPoint.GetComponentInChildren<CarInfo>().specificationsInt[j]);
                }
            }
            
        }
    }
}

