using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarImgInfo : MonoBehaviour
{
    [SerializeField] public CarSO carSO;
    [SerializeField] public TextMeshProUGUI priceText;
    public List<float> modsRating = new List<float>();

    private void Start()
    {
        priceText.text = carSO.price.ToString();
        modsRating.Add(carSO.ratingTire);
        modsRating.Add(carSO.ratingEngine);
        modsRating.Add(carSO.ratingBrake);
        modsRating.Add(carSO.ratingPendant);
        modsRating.Add(carSO.ratingTurbine);
    }
    
}
