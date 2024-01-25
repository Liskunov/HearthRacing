using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CarInfo", menuName = "ItemInfo/Car")]
public class CarInfo : ScriptableObject
{
   public string name;
   public Sprite icon;
   public int price = 0;
   public int maxSpeed = 0;
   public int boost = 0;
   public int braking = 0;


   public string Name
   {
      get
      {
         return name;
      }
   }

   public Sprite Icon
   {
      get
      {
         return icon;
      }
   }

   
   public int Price
   {
      get
      {
         return price;
      }
   }

   public int MaxSpeed
   {
      get
      {
         return maxSpeed;
      }
   }

   public int Boost
   {
      get
      {
         return boost;
      }
   }

   public int Braking
   {
      get
      {
         return braking;
      }
   }

}
