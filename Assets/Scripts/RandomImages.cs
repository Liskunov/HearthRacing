using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomImages : MonoBehaviour
{
    public GameObject[] carSlotObj;
    public GameObject[] modSlotObj;
    public TextMeshProUGUI moneyText;
    public int rerollCost;
    public void Spawn()
    {
        

        for (int i = 0; i < carSlotObj.Length; i++)
        {
            RemoveChildren(carSlotObj[i]);
            var CarsImg = Resources.LoadAll("ImageCars/CarsTier" + StaticInfo.lvlTav);
            var index = Random.Range(0, CarsImg.Length);
            Instantiate((CarsImg[index]), carSlotObj[i].transform);
            
        }
        
        


        for (int i = 0; i < modSlotObj.Length; i++)
        {
            RemoveChildren(modSlotObj[i]);
            var ModsImg = Resources.LoadAll("ImageMods/ModsTier" + StaticInfo.lvlTav);
            var index = Random.Range(0, ModsImg.Length);
            Instantiate((ModsImg[index]), modSlotObj[i].transform);
        }
    }

    private void Start()
    {
        Spawn();
    }

    public void RerollButton()
    {
        int money = int.Parse(moneyText.text);
        if (money >= rerollCost)
        {
            moneyText.text = (money - rerollCost).ToString();
            Spawn();
        }
    }

    public static void RemoveChildren(GameObject parent)
    {
        Transform transform;
        for(int i = 0;i < parent.transform.childCount; i++)
        {
            transform = parent.transform.GetChild(i);
            Destroy(transform.gameObject);
        }

    }
}
