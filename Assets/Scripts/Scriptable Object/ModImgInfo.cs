using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModImgInfo : MonoBehaviour
{
    [SerializeField] public ModSO modSO;
    [SerializeField] public TextMeshProUGUI priceText;
    [HideInInspector] public float rating;
    
    public List<int> specificationsModImg = new List<int>();

    private void Start()
    {
        priceText.text = modSO.price.ToString();
        rating = modSO.ratingMod;
        
        
        specificationsModImg.Add(modSO.MaxSpeed);
        specificationsModImg.Add(modSO.AccelerationMultiplier);
        specificationsModImg.Add(modSO.BrakeForce);
    }
}
