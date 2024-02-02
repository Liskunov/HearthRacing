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

    public bool ChangeGold(int price)
    {
        int money = int.Parse(moneyText.text);
        if (money >= price)
        {
            moneyText.text = (money - price).ToString();
            return true;
        }

        return false;
    }
}
