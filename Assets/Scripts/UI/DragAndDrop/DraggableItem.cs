using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform parentBeforeDrag;
    [SerializeField] public bool canBuy = true;
    public GameObject sellZone;


    public void Start()
    {
        sellZone = GameObject.Find("SellZone");
    }





    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        parentBeforeDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        
        
        if (!canBuy)
            sellZone.GetComponent<Image>().raycastTarget = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;


        if (!canBuy)
        {
            var f = (Vector3.Distance(transform.position, sellZone.transform.position)) / 1000f;
            if (f > 0.2f)
                sellZone.GetComponent<Image>().color = new Color(255, 255, 255, 0.25f);
            else
                sellZone.GetComponent<Image>().color = new Color(255, 255, 255, 1.25f - f);
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        if (!GetComponentInParent<InventorySlot>().canTake)
            image.raycastTarget = false;
        
        
        
        
        
        if (GetComponentInParent<InventorySlot>().swap)
        {
            parentAfterDrag.GetComponent<MoveCarMod>().TakeMods();
            parentBeforeDrag.GetComponent<MoveCarMod>().TakeMods();
            GetComponentInParent<InventorySlot>().swap = false;
        } TakeInfoMod();


        if (GetComponentInParent<SellCar>())
            GetComponentInParent<SellCar>().SellCars();
        
        
        
        sellZone.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        sellZone.GetComponent<Image>().raycastTarget = false;

    }

    public void TakeInfoMod()
    {
        for (int i = 1; i < 4; i++)
        {
            var f = GameObject.Find("ModInfo" + i);

            if (f)
                f.GetComponent<RatingManager>().TakeRating();
        }
    }

}

