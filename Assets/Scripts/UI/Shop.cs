using System.Collections;
using System.Collections.Generic;
using Cars;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private int price { get; set; }

    private void Start()
    {
        Invoke("GetPrice",0);
    }


    public void Sell()
    {
        int money = int.Parse(moneyText.text);
        if (money >= price)
        {
            moneyText.text = (money - price).ToString();
        } else
            return;
    }

    public void GetPrice()
    {
        price = GetComponentInChildren<Car>().carInfo.price;
        Debug.Log(price);
    }
}
