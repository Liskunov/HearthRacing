using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceThreeCars : MonoBehaviour
{
    public GameObject[] carSlotTake;
    public GameObject[] carSlotSpawn;
    public GameObject ui;
    public int index = 0;
    public List<string> indexCar = new List<string>();

    public void SpawnCars()
    {
        if (index == indexCar.Count)
        {
            for (int i = 0; i < indexCar.Count; i++)
            {
                var name = indexCar[i];
                var mod = Instantiate((Resources.Load("ImageCars/Test/" + name + "Img")), carSlotSpawn[i].transform);
                mod.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                mod.GetComponent<DraggableItem>().canBuy = false;
                mod.GetComponent<Image>().raycastTarget = false;
            }
            
            ui.SetActive(true);
            //carSlotSpawn[0].GetComponentInChildren<DraggableItem>().TakeInfoMod();
            gameObject.SetActive(false);
        }


        for (int i = 0; i < carSlotTake.Length; i++)
        {
            var CarsImg = Resources.LoadAll("ImageCars/Test");
            var index = Random.Range(0, CarsImg.Length);
            var mod = (CarsImg[index]).GetComponent<CarImgInfo>().carImgSO.icon;
            carSlotTake[i].GetComponent<Image>().sprite = mod;
        }
    }

    public void SpawnCarInHand()
    {
       // Take
    }

    public void Start()
    {
        SpawnCars();
    }
    
}
