using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor;
using UnityEngine;

public class RandomImages : MonoBehaviour
{
    public GameObject[] carSlotObj;
    public GameObject[] modSlotObj;
    public void Spawn()
    {

        string folderWithCarImg = "Assets/Prefabs/ImageCars/Test";

        for (int i = 0; i < carSlotObj.Length; i++)
        {
            RemoveChildren(carSlotObj[i]);
            string[] assetPaths = AssetDatabase.FindAssets("", new[] {folderWithCarImg});
            var randomIndex = Random.Range(0, assetPaths.Length);
            var path = AssetDatabase.GUIDToAssetPath(assetPaths[randomIndex]);
            Instantiate(PrefabUtility.LoadPrefabContents(path), carSlotObj[i].transform);
        }
        
        
        string folderWithModImg = "Assets/Prefabs/ImageMods";


        for (int i = 0; i < modSlotObj.Length; i++)
        {
            RemoveChildren(modSlotObj[i]);
            string[] assetPaths = AssetDatabase.FindAssets("", new[] {folderWithModImg});
            var randomIndex = Random.Range(0, assetPaths.Length);
            var path = AssetDatabase.GUIDToAssetPath(assetPaths[randomIndex]);
            Instantiate(PrefabUtility.LoadPrefabContents(path), modSlotObj[i].transform);
        }
    }

    private void Start()
    {
        Spawn();
    }

    public static void RemoveChildren(GameObject parent)
    {
        Transform transform;
        for(int i = 0;i < parent.transform.childCount; i++)
        {
            transform = parent.transform.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }

    }
}
