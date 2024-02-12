using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CarInfo", menuName = "CarSO")]
public class CarImgSO : ScriptableObject
{
   public string nameCar;
   public Sprite icon;
   public int price = 0;
   public float ratingTire = 0.1f;
   public float ratingEngine = 0.1f;
   public float ratingBrake = 0.1f;
   public float ratingPendant = 0.1f;
   public float ratingTurbine = 0.1f;
   
   [Range(20, 190)] public int MaxSpeed = 1;
   [Range(1, 12)] public int AccelerationMultiplier = 1;
   [Range(100, 600)] public int BrakeForce = 1;
  


   public string NameCar
   {
      get
      {
         return nameCar;
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
   
   public float RatingTire
   {
      get
      {
         return ratingTire;
      }
   }
   
   public float RatingEngine
   {
      get
      {
         return ratingEngine;
      }
   }
   
   public float RatingBrake
   {
      get
      {
         return ratingBrake;
      }
   }
   
   public float RatingPendant
   {
      get
      {
         return ratingPendant;
      }
   }

   public float RatingTurbine
   {
      get
      {
         return ratingTurbine;
      }
   }

}