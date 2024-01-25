using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    [SerializeField] public CarInfo carInfo;
    [SerializeField] public TextMeshProUGUI priceText;

    private void Start()
    {
        priceText.text = carInfo.price.ToString();
    }
    
}
