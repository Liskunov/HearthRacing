using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarImgInfo : MonoBehaviour
{
    [SerializeField] public CarImgSO carImgSO;
    [SerializeField] public TextMeshProUGUI priceText;
    public List<float> modsRating = new List<float>();
    public List<string> modsNames = new List<string>();
    public List<int> specificationsCarImg = new List<int>();

    private void Start()
    {
        priceText.text = carImgSO.price.ToString();
        
        
        modsRating.Add(carImgSO.ratingTire);
        modsRating.Add(carImgSO.ratingEngine);
        modsRating.Add(carImgSO.ratingBrake);
        modsRating.Add(carImgSO.ratingPendant);
        modsRating.Add(carImgSO.ratingTurbine);
        
        specificationsCarImg.Add(carImgSO.MaxSpeed);
        specificationsCarImg.Add(carImgSO.AccelerationMultiplier);
        specificationsCarImg.Add(carImgSO.BrakeForce);

    }
    
}
