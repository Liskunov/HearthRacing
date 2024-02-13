
using System.Collections.Generic;
using UnityEngine;

public static class StaticInfo
{
    public static int lvlTav = 1;
    public static int enemyHP = 100;
    public static int playerHPs = 100;
    public static GameObject mapsManager;
    public static List<GameObject> spawnPoints = new List<GameObject>(new GameObject[3]);
    public static List<GameObject> spawnPointsEnemy = new List<GameObject>(new GameObject[3]);
    public static float time;
    public static int numberRound = 1;
    public static GameObject playUI;
    public static GameObject SwitchManager;
}
