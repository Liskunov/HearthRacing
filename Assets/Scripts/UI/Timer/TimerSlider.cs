using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerSlider : MonoBehaviour
{
    public Slider slider;

    public float lifeTime = 60f;
    private float gameTime;

    private void Update()
    {
        slider.value = lifeTime;
        gameTime += 1 * Time.deltaTime;
        if (gameTime >= 1)
        {
            lifeTime -= 1;
            gameTime = 0;
        }
    }
}
