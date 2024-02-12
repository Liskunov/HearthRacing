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
        for (int i = 0; i < StaticInfo.spawnPoints.Count; i++)
        {
            var spawnPoint = StaticInfo.spawnPoints[i];
            
            if (spawnPoint.transform.childCount != 0)
            {
                Transform transform = spawnPoint.transform.GetChild(0);
                DestroyImmediate(transform.gameObject);
            } 
            
            if (ratingManagers[i].CarSlot.transform.childCount != 0)
            {
                var carImgInfo = ratingManagers[i].CarSlot.GetComponentInChildren<CarImgInfo>();

                var nameCar = carImgInfo.carImgSO.nameCar;
                var car = Instantiate(Resources.Load<GameObject>("Cars/"+ nameCar), spawnPoint.transform);
                var carInfo = car.GetComponent<CarInfo>();



                for (int j = 0; j < carImgInfo.specificationsCarImg.Count; j++)
                {
                    carInfo.specificationsC[j] = carImgInfo.specificationsCarImg[j];
                    carInfo.carName = carImgInfo.carImgSO.nameCar;
                    carInfo.carImage = carImgInfo.carImgSO.Icon;
                }

                carInfo.LoadMod();
            }
        }
    }
}

