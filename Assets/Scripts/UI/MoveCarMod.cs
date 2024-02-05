using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveCarMod : MonoBehaviour
{
   [SerializeField] public GameObject[] modsSlots;
   
   
   public void TakeMods()
   {
      for (int i = 0; i < GetComponentInChildren<CarImgInfo>().modsNames.Count; i++)
      {
         if (modsSlots[i].transform.childCount != 0)
         {
            Transform transform = modsSlots[i].transform.GetChild(0);
            GameObject.Destroy(transform.gameObject);
         }
         
         
         if (GetComponentInChildren<CarImgInfo>().modsNames[i] != "")
         {
            string name = GetComponentInChildren<CarImgInfo>().modsNames[i];

            Instantiate(PrefabUtility.LoadPrefabContents("Assets/Prefabs/ImageMods/" + name + ".prefab"), modsSlots[i].transform);
         }
      }
   }
}
