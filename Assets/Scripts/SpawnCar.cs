using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    [SerializeField] public RatingManager[] ratingManagers;
    public GameObject spawnPoints;

    public void Start()
    {
        spawnPoints = GameObject.Find("SpawnPoints");
    }


    public void SpawnCarInPoint()
    {
        for (int i = 0; i < ratingManagers.Length; i++)
        {
            var spawnPoint = spawnPoints.transform.GetChild(i);
            
            if (spawnPoint.transform.childCount != 0)
            {
                Transform transform = spawnPoint.transform.GetChild(0);
                Destroy(transform.gameObject);
            } 
            
            if (ratingManagers[i].CarSlot.transform.childCount != 0)
            {
                var carImgInfo = ratingManagers[i].CarSlot.GetComponentInChildren<CarImgInfo>();

                var nameCar = carImgInfo.carImgSO.nameCar;
                var car = Instantiate(Resources.Load("Cars/CarTier1/" + nameCar), spawnPoint.transform);
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

