using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SellCar : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI moneyText;
   [SerializeField] public TextMeshProUGUI sellPriceText;
   [SerializeField] private float sellRatio;

   public void SellCars()
   {
      var sellObject = GetComponentInChildren<DraggableItem>().gameObject;
      int sellPrice = 0;
      
      
      if (sellObject.GetComponent<ModImgInfo>())
         sellPrice = sellObject.GetComponent<ModImgInfo>().modSO.price;
      else
      {
         for (int i = 0; i < sellObject.GetComponent<CarImgInfo>().priceModsInCar.Count; i++)
         {
            sellPrice += sellObject.GetComponent<CarImgInfo>().priceModsInCar[i];
         }
         sellPrice += sellObject.GetComponent<CarImgInfo>().carImgSO.price;
         
         sellObject.GetComponent<DraggableItem>().parentBeforeDrag.GetComponentInChildren<MoveCarMod>().Delete();
         
      }

      
      Destroy(sellObject);
      moneyText.text = ((int)(int.Parse(moneyText.text) + sellPrice*sellRatio)).ToString();
      
   }



}
