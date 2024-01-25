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
    
    
    public void Sell()
    {
        int money = int.Parse(moneyText.text);
        moneyText.text = (money - GetComponentInChildren<Car>().carInfo.price).ToString();
        Debug.Log(moneyText);
    }
}
