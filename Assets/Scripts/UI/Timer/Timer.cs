using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;

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
    }

}
