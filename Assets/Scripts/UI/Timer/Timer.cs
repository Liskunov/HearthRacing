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

    [SerializeField] public float time;
    
    [SerializeField] private TextMeshProUGUI[] timerText;
    [SerializeField] private Slider[] timerSlider;
    public GameObject spwManager;
    [SerializeField] public GameObject[] UI;

    private float _timeLeft = 0f;
 
    public IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            var normalizedValue = Mathf.Clamp(_timeLeft / time, 0.0f, 1.0f);
            foreach (var timer in timerSlider)
                timer.value = normalizedValue;
            UpdateTimeText();
            yield return null;
        }
    }
 
    private void Start()
    {
        LoadTimer();
        StaticInfo.time = time;
    }

    public void LoadTimer()
    {
        _timeLeft = time;
        StartCoroutine(StartTimer());
    }

    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
        {
            _timeLeft = 0;
            spwManager.GetComponent<SpawnCar>().SpawnCarInPoint();
            spwManager.GetComponent<SpawnCarEnemy>().SpawnEnemyCar();
            for (int i = 0; i < UI.Length; i++)
            {
                UI[i].SetActive(false);
            }

            for (int i = 0; i < StaticInfo.spawnPoints.Count; i++)
            {
                var spawnPoint = StaticInfo.spawnPoints[i];
                var spawnEnemyPoint = StaticInfo.spawnPointsEnemy[i];
                spawnPoint.GetComponentInChildren<CarAI>().enabled = true;
                spawnEnemyPoint.GetComponentInChildren<CarAI>().enabled = true;

                StaticInfo.SwitchManager.GetComponent<CameraSwitch>().FirstCam();

            }

        }


        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        foreach (var text in timerText)
        text.text = seconds.ToString();
    }
}
