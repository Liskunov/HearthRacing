using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    [SerializeField] private GameObject[] carsImg;
    [SerializeField] private GameObject[] cars;

    public void Update()
    {
        for (int i = 0; i < carsImg.Length; i++)
        {
            if (carsImg[i].transform.parent.name == "UISlot1")
            {
                cars[i].SetActive(true);
            }
            else cars[i].SetActive(false);
        }

    }
}
