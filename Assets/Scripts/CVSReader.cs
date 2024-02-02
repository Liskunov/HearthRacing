using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CVSReader : MonoBehaviour
{
  public TextAsset TextAssetData;

  [System.Serializable]

  public class Player
  {
    public string name;
    public int maxspeed;
    public int size;
    public int rating;
  }

  [System.Serializable]
  public class PlayerList
  {
    public Player[] mods;
  }
  
  public PlayerList myModList = new PlayerList();

  void Start()
  {
    ReadCSV(); 
  }

  void ReadCSV()
  {
    string[] data = TextAssetData.text.Split(new string[] {";", "\n"}, StringSplitOptions.None);
    int tableSize = data.Length / 4 - 1;
    myModList.mods = new Player[tableSize];

    for (int i = 0; i < tableSize; i++)
    {
      myModList.mods[i] = new Player();
      myModList.mods[i].name = data[4 * (i + 1)];
      myModList.mods[i].maxspeed = int.Parse(data[4 * (i + 1) + 1]);
      myModList.mods[i].size = int.Parse(data[4 * (i + 1) + 2]);
      myModList.mods[i].rating = int.Parse(data[4 * (i + 1) + 3]);
    }
  }
  
}
