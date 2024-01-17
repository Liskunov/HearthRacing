using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class SpawnCarFromShop : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject cars;


    private void Spawn()
    {
        Start();
    }

    private void Start()
    {
        string folderWithCar = "Assets/Prefabs/Cars/Test";
        
        
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            string[] assetPaths = AssetDatabase.FindAssets("", new[] {folderWithCar});
            var randomIndex = assetPaths.Length;
            var path = AssetDatabase.GUIDToAssetPath(assetPaths[randomIndex]);
            Instantiate(PrefabUtility.LoadPrefabContents(path), spawnPoints[i]);
        }
    }

    public void Update()
    {
       // if (gameObject.GetComponentInParent(tag))
    }
}
