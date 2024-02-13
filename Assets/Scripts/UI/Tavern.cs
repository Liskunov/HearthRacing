using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Tavern : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public int costUpTav;
    public TextMeshProUGUI tavLvlText;
    
    
    public void UpTav()
    {
        int money = int.Parse(moneyText.text);
        if (money >= costUpTav && StaticInfo.lvlTav < 3)
        {
            moneyText.text = (money - costUpTav).ToString();
            StaticInfo.lvlTav++;
            tavLvlText.text = StaticInfo.lvlTav.ToString();
            costUpTav += 200;
        }
    }
}
