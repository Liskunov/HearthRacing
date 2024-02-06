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
                Instantiate(Resources.Load("CarReady/" + nameCar), spawnPoint.transform);
                
                
                for (int j = 0; j < ratingManagers[i].CarSlot.GetComponentInChildren<CarImgInfo>().specificationsCarImg.Count; j++)
                {
                    spawnPoint.GetComponentInChildren<CarInfo>().specificationsC[j] = ratingManagers[i].CarSlot.GetComponentInChildren<CarImgInfo>().specificationsCarImg[j];
                }
                
                spawnPoint.GetComponentInChildren<CarInfo>().LoadMod();
            }
        }
    }
}

