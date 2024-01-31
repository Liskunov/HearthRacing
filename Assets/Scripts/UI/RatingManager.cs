using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RatingManager : MonoBehaviour
{
    [SerializeField] public GameObject CarSlot;
    private float carslider = 0;

        public void Update()
    {
      
        
        
        
        for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (CarSlot.transform.childCount != 0)
                {
                    carslider = CarSlot.GetComponentInChildren<CarImgInfo>().modsRating[i];
                    gameObject.transform.GetChild(i).GetChild(1).GetComponent<Slider>().value = carslider;
                }




                if (gameObject.transform.GetChild(i).GetChild(0).childCount != 0)
                {
                    float f = gameObject.transform.GetChild(i).GetChild(0).GetComponentInChildren<ModImgInfo>().rating;
                    gameObject.transform.GetChild(i).GetChild(1).GetComponent<Slider>().value = f + carslider;
                }
                else gameObject.transform.GetChild(i).GetChild(1).GetComponent<Slider>().value = carslider;
            }
    }
}
