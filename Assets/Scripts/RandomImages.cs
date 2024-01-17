using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor;
using UnityEngine;

public class RandomImages : MonoBehaviour
{
    public Transform[] carSlotTransform;
    public GameObject[] carSlotObj;
    public Transform[] modSlotTransform;
    public GameObject[] modSlotObj;
    public void Spawn()
    {

        string folderWithCarImg = "Assets/Prefabs/ImageCars/Test";

        for (int i = 0; i < carSlotTransform.Length; i++)
        {
            carSlotObj[i].transform.DetachChildren();
            string[] assetPaths = AssetDatabase.FindAssets("", new[] {folderWithCarImg});
            var randomIndex = Random.Range(0, assetPaths.Length);
            var path = AssetDatabase.GUIDToAssetPath(assetPaths[randomIndex]);
            Instantiate(PrefabUtility.LoadPrefabContents(path), carSlotTransform[i]);
        }
        
        
        string folderWithModImg = "Assets/Prefabs/ImageMods";


        for (int i = 0; i < modSlotTransform.Length; i++)
        {
            modSlotObj[i].transform.DetachChildren();
            string[] assetPaths = AssetDatabase.FindAssets("", new[] {folderWithModImg});
            var randomIndex = Random.Range(0, assetPaths.Length);
            var path = AssetDatabase.GUIDToAssetPath(assetPaths[randomIndex]);
            Instantiate(PrefabUtility.LoadPrefabContents(path), modSlotTransform[i]);
        }
    }

    private void Start()
    {
        Spawn();
    }
}
