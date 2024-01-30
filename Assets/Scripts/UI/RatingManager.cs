using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RatingManager : MonoBehaviour
{
   [SerializeField] private GameObject[] slots;
   [SerializeField] private GameObject[] scrolls;

   public void Update()
   {
      for (int i = 0; i < slots.Length; i++)
      {
         if (slots[i].transform.childCount != 0)
            scrolls[i].GetComponent<Slider>().value = slots[i].GetComponentInChildren<ItemImgInfo>().rating;
            
         else scrolls[i].GetComponent<Slider>().value = 0;
      }
   }
}
