using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
   public List<GameObject> cars = new List<GameObject>();
   public CarFinishInfo finishManager;
   int playerPoints = 0;
   int enemyPoints = 0;


   void OnTriggerEnter(Collider other)
   {
       var carName = other.gameObject.GetComponent<CarInfo>().carName;
      finishManager.SpawnCarInfo(carName, other.gameObject.tag);
      cars.Add(other.gameObject);
      if (cars.Count == 6)
      {
          HealthPoints();
          if (finishManager.GetComponent<HPController>().HPCalculation(playerPoints, enemyPoints))
          finishManager.GetComponent<EndInfo>().TakeInfo(cars, playerPoints, enemyPoints);
          StaticInfo.SwitchManager.GetComponent<CameraSwitch>().UI.SetActive(false);

      }
   }

   void HealthPoints()
   {
       int points = 6;
       
       for (int i = 0; i < cars.Count; i++)
       {
           if (cars[i].tag == "CarPlayer")
           {
               playerPoints += points;
               points--;
           }
           else
           {
               enemyPoints += points;
               points--;
           }
       }
   }
}
