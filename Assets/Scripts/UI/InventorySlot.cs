using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using TMPro;
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

        if (!m_stringTags.Any(str => dropped.CompareTag(str)))
            return;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        draggableItem.parentAfterDrag = transform;

        if (transform.childCount != 0)
        {
            var child = transform.GetChild(0);
            child.transform.SetParent(draggableItem.parentBeforeDrag);
            
        }
    }
}
