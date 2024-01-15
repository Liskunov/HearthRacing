using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor;
using UnityEngine;

public class RandomImages : MonoBehaviour
{
    public Transform[] carSlotTransform;
    public Transform[] modSlotTransform;
    private void Start()
    {
        string folderWithCarImg = "Assets/Prefabs/ImageCars";


        for (int i = 0; i < carSlotTransform.Length; i++)
        {
            string[] assetPaths = AssetDatabase.FindAssets("", new[] {folderWithCarImg});
            var randomIndex = Random.Range(0, assetPaths.Length);
            var path = AssetDatabase.GUIDToAssetPath(assetPaths[randomIndex]);
            Instantiate(PrefabUtility.LoadPrefabContents(path), carSlotTransform[i]);
        }
        
        
        string folderWithModImg = "Assets/Prefabs/ImageMods";


        for (int i = 0; i < modSlotTransform.Length; i++)
        {
            string[] assetPaths = AssetDatabase.FindAssets("", new[] {folderWithModImg});
            var randomIndex = Random.Range(0, assetPaths.Length);
            var path = AssetDatabase.GUIDToAssetPath(assetPaths[randomIndex]);
            Instantiate(PrefabUtility.LoadPrefabContents(path), modSlotTransform[i]);
        }
    }
}
