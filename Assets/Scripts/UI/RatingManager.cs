using UnityEngine;


public class RatingManager : MonoBehaviour
{
    [SerializeField] public GameObject CarSlot;
    public ModItem[] modItems { private set; get; }

    private void Awake()
    {
        modItems = GetComponentsInChildren<ModItem>();
    }

    public void TakeRating()
    {
        var carImg = CarSlot.GetComponentInChildren<CarImgInfo>();

        for (int i = 0; i < modItems.Length; i++)
        {

            if (!modItems[i].inventorySlot.GetComponentInChildren<ModImgInfo>())
            {
                if (carImg)
                {
                    carImg.modsNames[i] = "";
                    carImg.priceModsInCar[i] = 0;
                }

            }
            else
            {
                carImg.modsNames[i] = modItems[i].inventorySlot.GetComponentInChildren<ModImgInfo>().name;
                carImg.priceModsInCar[i] = modItems[i].inventorySlot.GetComponentInChildren<ModImgInfo>().modSO.price;
                for (int j = 0; j < carImg.specificationsCarImg.Count; j++)
                {
                     carImg.specificationsCarImg[j] += modItems[i].inventorySlot.GetComponentInChildren<ModImgInfo>().specificationsModImg[j];
                }
            }
        }
    }

    public void RatingMods()
    {
        var carImg = CarSlot.GetComponentInChildren<CarImgInfo>();
        for (int i = 0; i < modItems.Length; i++)
            if (!modItems[i].inventorySlot.GetComponentInChildren<ModImgInfo>())
            {
                if (carImg)
                    modItems[i].slider.value = carImg.modsRating[i];
            }
            else
                modItems[i].slider.value = modItems[i].inventorySlot.GetComponentInChildren<ModImgInfo>().rating + carImg.modsRating[i];
    }
}
