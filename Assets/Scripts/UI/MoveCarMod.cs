using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCarMod : MonoBehaviour
{
   [SerializeField] public GameObject[] modsSlots;
   private int f;
   
   
   public void TakeMods()
   {
      f = GetComponentInChildren<CarImgInfo>().modsNames.Count;
      
      
      for (int i = 0; i < GetComponentInChildren<CarImgInfo>().modsNames.Count; i++)
      {
         if (modsSlots[i].transform.childCount != 0)
         {
            Transform transform = modsSlots[i].transform.GetChild(0);
            DestroyImmediate(transform.gameObject); 
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

   public void Delete()
   {

      for (int i = 0; i < f; i++)
      {
         if (modsSlots[i].transform.childCount != 0)
         {
            Transform transform = modsSlots[i].transform.GetChild(0);
            DestroyImmediate(transform.gameObject);
         }
      }
   }
}
