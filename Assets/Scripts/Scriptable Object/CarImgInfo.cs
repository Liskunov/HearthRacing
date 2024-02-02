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
    public List<float> specifications = new List<float>();

    private void Start()
    {
        priceText.text = carImgSO.price.ToString();
        
        
        modsRating.Add(carImgSO.ratingTire);
        modsRating.Add(carImgSO.ratingEngine);
        modsRating.Add(carImgSO.ratingBrake);
        modsRating.Add(carImgSO.ratingPendant);
        modsRating.Add(carImgSO.ratingTurbine);
        
        modsNames.Add("none");
        modsNames.Add("none");
        modsNames.Add("none");
        modsNames.Add("none");
        modsNames.Add("none");
        
        specifications.Add(carImgSO.ratingTire);
        specifications.Add(carImgSO.ratingEngine);
        specifications.Add(carImgSO.ratingBrake);
        specifications.Add(carImgSO.ratingPendant);
        specifications.Add(carImgSO.ratingTurbine);
        
    }
    
}
