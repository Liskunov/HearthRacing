using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
   public List<GameObject> cars = new List<GameObject>();
   public CarFinishInfo finishManager;


   void OnTriggerEnter(Collider other)
   {
      finishManager.SpawnCarInfo(other.gameObject.name, other.gameObject.tag);
      cars.Add(other.gameObject);
      if (cars.Count == 6)
      {
          HealthPoints();
          return;
      }
   }

   void HealthPoints()
   {
       int points = 6;
       int playerPoints = 0;
       int enemyPoints = 0;

       
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
