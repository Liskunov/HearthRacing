using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    public GameObject looseOrWinPanel;

    public bool HPCalculation(int playerPoints, int enemyPoints)
    {
        var points = playerPoints - enemyPoints;
        
        
        if (points > 0)
            StaticInfo.enemyHP -= points*5;
        else
            StaticInfo.playerHPs -= Math.Abs(points*5);

        Debug.Log(StaticInfo.playerHPs);
        
        if (StaticInfo.enemyHP < 0)
        {
            looseOrWinPanel.SetActive(true);
            looseOrWinPanel.GetComponent<Image>().color = Color.green;
            looseOrWinPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You win";
            StartCoroutine(LoadMenu());
            return false;
        } else 
        
        if (StaticInfo.playerHPs < 0)
        {
            looseOrWinPanel.SetActive(true);
            looseOrWinPanel.GetComponent<Image>().color = Color.red;
            looseOrWinPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You loose";
            StartCoroutine(LoadMenu());
            return false;
        } else

        return true;
    }


    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(5f);
        Destroy(StaticInfo.playUI);
        SceneManager.LoadScene("Menu");
        
    StaticInfo.lvlTav = 1;
    StaticInfo.enemyHP = 100;
    StaticInfo.playerHPs = 100;
    StaticInfo.numberRound = 1;
    }

}
