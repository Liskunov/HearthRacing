using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
            name = name.Substring(0, name.Length - 7);
            
            var mod= Instantiate((Resources.Load("ImageMods/" + name)), modsSlots[i].transform);
            mod.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            mod.GetComponent<DraggableItem>().canBuy = false;
         }
      }
   }
}
