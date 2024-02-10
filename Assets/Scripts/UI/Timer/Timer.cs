using System.Collections;
using System.Collections.Generic;
using Cars;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public SpawnCar spawnCar;
    
    [SerializeField] public GameObject[] UI;

    public float lifeTime = 60f;
    private float gameTime;

    private void FixedUpdate()
    {
        timer.text = "Time:" + lifeTime + " sec";
        gameTime += 1 * Time.fixedDeltaTime;
        if (gameTime >= 1)
        {
            lifeTime -= 1;
            gameTime = 0;
        }

        if (lifeTime <= 5)
        {
            timer.color = Color.yellow;
        } else timer.color = Color.green;

        if (lifeTime <= 3)
        {
            timer.color = Color.red;
        }

        if((gameTime <= 0.04f) && lifeTime == 0)
        spawnCar.SpawnCarInPoint();
        
        
        if (lifeTime == 0)
        {

            for (int i = 0; i < UI.Length; i++)
            {
                UI[i].SetActive(false);
            }
            

            for (int i = 0; i < 3; i++)
            {
                var spawnPoint = GameObject.Find("SpawnPoint" + i);
                if(spawnPoint.transform.childCount != 0)
                spawnPoint.GetComponentInChildren<CarAI>().enabled = true;

            }
        }
    }

}
