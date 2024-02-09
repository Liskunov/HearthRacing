using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeSpriteInfo : MonoBehaviour
{
    public void TakeInfo()
    {
        var carName = gameObject.GetComponent<Image>().sprite.name;
        var index = GetComponentInParent<ChoiceThreeCars>().index;
        GetComponentInParent<ChoiceThreeCars>().indexCar[index] = carName;
        GetComponentInParent<ChoiceThreeCars>().index++;
        GetComponentInParent<ChoiceThreeCars>().SpawnCars();
    }
}
