using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextMap : MonoBehaviour
{
    public GameObject UI;
    public Timer timer;
    
    void Start()
    {
        StaticInfo.mapsManager = gameObject;
    }

    public void LoadNextScene()
    {
        Debug.Log(StaticInfo.numberRound);
        if (StaticInfo.numberRound < 3)
            SceneManager.LoadScene("Track3");
        else
            SceneManager.LoadScene("Track2");
        StaticInfo.numberRound++;
        UI.SetActive(true);
        timer.time = StaticInfo.time;
        timer.LoadTimer();
        GetComponentInChildren<Shop>().GetMoney();
    }

}
