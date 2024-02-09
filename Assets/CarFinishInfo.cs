using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarFinishInfo : MonoBehaviour
{
    public GameObject finishUI;
    private Object carInfo;
    
    public void SpawnCarInfo(string carName, string carTag)
    {
        carInfo = Instantiate(Resources.Load("Image/FinishedCar"), finishUI.transform);
        carInfo.GetComponentInChildren<RawImage>().texture = Resources.Load<Texture>("ImageCarsMini/" + carName);
        carInfo.GetComponentInChildren<TextMeshProUGUI>().text = carName;
        if (carTag == "CarPlayer")
        carInfo.GetComponent<Image>().color = Color.green;
        
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        var carInfo1 = carInfo;
        yield return new WaitForSeconds(5f);
        Destroy(carInfo1);
    }
}
