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
    
    [SerializeField] private TextMeshProUGUI timerText;
    public SpawnCar spawnCar;
    [SerializeField] public GameObject[] UI;
 
    private float _timeLeft = 0f;
 
    public IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }
    }
 
    private void Start()
    {
        _timeLeft = time;
        StaticInfo.time = time;
        StartCoroutine(StartTimer());
    }
 
    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
        {
            _timeLeft = 0;
            spawnCar.SpawnCarInPoint();
            for (int i = 0; i < UI.Length; i++)
            {
                UI[i].SetActive(false);
            }

            for (int i = 0; i < StaticInfo.spawnPoints.Count; i++)
            {
                var spawnPoint = StaticInfo.spawnPoints[i];
                spawnPoint.GetComponentInChildren<CarAI>().enabled = true;

            }
        }


        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = seconds.ToString();
    }
}
