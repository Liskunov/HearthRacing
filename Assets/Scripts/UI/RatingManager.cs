using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingManager : MonoBehaviour
{
   [SerializeField] private GameObject[] slots;
   [SerializeField] private GameObject[] scrolls;

   public void SetRating()
   {
      for (int i = 0; i < slots.Length; i++)
      {
         if (slots[i].transform.childCount != 0)
         {
            float f = slots[i].GetComponentInChildren<ItemImgInfo>().rating;
            scrolls[i].GetComponent<Slider>().value = f;
         } 
         else scrolls[i].GetComponent<Slider>().value = 0;
      }
   }
}
