using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor;
using UnityEngine;

public class RandomImages : MonoBehaviour
{
    public Transform[] parentTransform;
    private void Start()
    {
        string folderWithContent = "Assets/Prefabs/ImageCars";


        for (int i = 0; i < parentTransform.Length; i++)
        {
            string[] assetPaths = AssetDatabase.FindAssets("", new[] {folderWithContent});
            var randomIndex = Random.Range(0, assetPaths.Length);
            var path = AssetDatabase.GUIDToAssetPath(assetPaths[randomIndex]);
            Instantiate(PrefabUtility.LoadPrefabContents(path), parentTransform[i]);
        }
    }
}
