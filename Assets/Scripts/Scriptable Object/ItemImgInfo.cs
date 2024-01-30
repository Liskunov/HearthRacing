using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemImgInfo : MonoBehaviour
{
    [SerializeField] public ItemSO itemSO;
    [SerializeField] public TextMeshProUGUI priceText;
    public float rating;

    private void Start()
    {
        priceText.text = itemSO.price.ToString();
        rating = itemSO.rating;
    }
    
}
