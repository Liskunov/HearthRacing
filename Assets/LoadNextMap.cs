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
        var scenes = Resources.LoadAll("Scenes");
        var index = Random.Range(0, scenes.Length);
        var sceneName = scenes[index].name;
        SceneManager.LoadScene("Track2");
        UI.SetActive(true);
        timer.time = StaticInfo.time;
        timer.StartTimer();
    }

}
