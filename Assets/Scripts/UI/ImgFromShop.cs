using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ImgFromShop : MonoBehaviour
{
    [SerializeField] private Transform[] OutCarImg;
    [SerializeField] private Transform[] InCarImg;

    private void Copy()
    {
        for (var i = 0; i < OutCarImg.Length; i++)
            Instantiate(OutCarImg[i], InCarImg[i]);
    }
}
