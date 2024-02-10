using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro.EditorUtilities;
using UnityEngine;

public static class StaticInfo
{
    public static int lvlTav = 1;
    public static int enemyHP = 100;
    public static int playerHP = 100;
    public static GameObject mapsManager;
    public static List<GameObject> spawnPoints = new List<GameObject>(new GameObject[3]);
    public static float time;
}
