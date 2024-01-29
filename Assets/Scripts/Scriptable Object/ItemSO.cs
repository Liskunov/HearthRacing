using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "ItemInfo")]
public class ItemSO : ScriptableObject
{
   public string name;
   public Sprite icon;
   public int price = 0;

   
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

}
