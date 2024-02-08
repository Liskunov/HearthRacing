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
    public ModItem[] modItems { private set; get; }

    private void Awake()
    {
        modItems = GetComponentsInChildren<ModItem>();
    }

    public void TakeRating()
    {
        var carImg = CarSlot.GetComponentInChildren<CarImgInfo>();

        for (int i = 0; i < modItems.Length; i++)
        {
            if (!modItems[i].inventorySlot.GetComponentInChildren<ModImgInfo>())
            {
                if (carImg)
                {
                    modItems[i].slider.value = carImg.modsRating[i];
                    carImg.modsNames[i] = "";
                    carImg.priceModsInCar[i] = 0;
                }

            }
            else
            {
                modItems[i].slider.value = modItems[i].inventorySlot.GetComponentInChildren<ModImgInfo>().rating + carImg.modsRating[i];
                carImg.modsNames[i] = modItems[i].inventorySlot.GetComponentInChildren<ModImgInfo>().name;
                carImg.priceModsInCar[i] = modItems[i].inventorySlot.GetComponentInChildren<ModImgInfo>().modSO.price;
                for (int j = 0; j < carImg.specificationsCarImg.Count; j++)
                {
                     carImg.specificationsCarImg[i] += modItems[i].inventorySlot.GetComponentInChildren<ModImgInfo>().specificationsModImg[j];
                }
            }
        }
    }
}
