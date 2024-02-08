using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private enum ShopTags
    {
        CarImg,
        TireImg,
        EngineImg,
        BrakeImg,
        PendantImg,
        TurbineImg
    }
    

    [SerializeField] private List<ShopTags> m_tags;
    [SerializeField] private Shop shop;
    [SerializeField] public bool canTake = true;
    [HideInInspector] public bool swap = false;
    


    private int droppedPrice = 0;

    private List<string> m_stringTags = new List<string>();
    
    private void Start()
    {
        foreach (var tag in m_tags)
        {
            m_stringTags.Add(tag.ToString());
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;

        if (!dropped.CompareTag("Untagged"))
        {
            if (dropped.CompareTag("CarImg"))
            {
                droppedPrice = int.Parse(dropped.GetComponent<CarImgInfo>().priceText.text);
            }
            else droppedPrice = int.Parse(dropped.GetComponent<ModImgInfo>().priceText.text);
        }

        if (!m_stringTags.Any(str => dropped.CompareTag(str)))
            return;
        if (dropped.GetComponent<DraggableItem>().canBuy && transform.childCount != 0)
            return;
        if (dropped.GetComponent<DraggableItem>().canBuy)
            if (!shop.ChangeGold(droppedPrice))
                return;


        dropped.GetComponent<DraggableItem>().canBuy = false;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        draggableItem.parentAfterDrag = transform;


        if (transform.childCount != 0 && transform.GetChild(0).tag != "CarImg")
        {
            var child = transform.GetChild(0);
            GetComponentInChildren<DraggableItem>().image.raycastTarget = true;
            child.transform.SetParent(draggableItem.parentBeforeDrag);
        }
        else 
        if (transform.childCount != 0)
        {
            var child = transform.GetChild(0);
            GetComponentInChildren<DraggableItem>().image.raycastTarget = true;
            child.transform.SetParent(draggableItem.parentBeforeDrag);

            swap = true;

        }
    }
}
