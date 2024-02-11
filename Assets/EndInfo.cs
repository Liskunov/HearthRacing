using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndInfo : MonoBehaviour
{
    public GameObject endPanel;

    public void TakeInfo(List<GameObject> cars, int playerPoints, int enemyPoints)
    {
        endPanel.SetActive(true);
         var panels = endPanel.GetComponentsInChildren<GridLayoutGroup>();
         var finishPointsSliders = endPanel.GetComponentsInChildren<Slider>();
         var points = playerPoints - enemyPoints;

         finishPointsSliders[0].value = (float) StaticInfo.playerHPs / 100;
         finishPointsSliders[1].value = (float) StaticInfo.enemyHP / 100;
         
         
         for (int i = 0; i < cars.Count; i++)
         {
             if (cars[i].tag == "CarPlayer")
                 {
                     SpawnPrefabCarImg(panels, cars, i, 0);
                 }
                 else
                 {
                     SpawnPrefabCarImg(panels, cars, i, 1);
                 }
         }
         
             finishPointsSliders[0].GetComponentInChildren<TextMeshProUGUI>().text = playerPoints.ToString();
             finishPointsSliders[1].GetComponentInChildren<TextMeshProUGUI>().text = enemyPoints.ToString();
    }


    public void SpawnPrefabCarImg(GridLayoutGroup[] panels, List<GameObject> cars, int i, int j)
    {
        var spawnImgCar = Instantiate(Resources.Load("EndImage/CarSlot"), panels[j].transform);
        var finishedCar = cars[i];
        spawnImgCar.GetComponentInChildren<TextMeshProUGUI>().text = finishedCar.GetComponent<CarInfo>().carName;
        spawnImgCar.GetComponentInChildren<Text>().text = (i+1).ToString();
        spawnImgCar.GetComponentInChildren<RawImage>().texture = finishedCar.GetComponent<CarInfo>().carImage.texture;
    }
    
}
