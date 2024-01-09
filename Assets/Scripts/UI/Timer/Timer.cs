using System.Collections;
using System.Collections.Generic;
using Cars;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    [SerializeField] public GameObject[] startScript;
    [SerializeField] public GameObject ui;

    public float lifeTime = 60f;
    private float gameTime;

    private void Update()
    {
        timer.text = "Time:" + lifeTime + " sec";
        gameTime += 1 * Time.deltaTime;
        if (gameTime >= 1)
        {
            lifeTime -= 1;
            gameTime = 0;
        }

        if (gameTime <= 5)
        {
            timer.color = Color.yellow;
        }

        if (gameTime <= 3)
        {
            timer.color = Color.red;
        }

        if (lifeTime <= 0)
        {
            for (int i = 0; i < startScript.Length; i++)
            startScript[i].GetComponent<CarAI>().enabled = true;
            ui.SetActive(false);
            
        }
    }

}
