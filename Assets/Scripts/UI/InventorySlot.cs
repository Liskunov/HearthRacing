using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private bool m_allowDrop = true;
    
    public void OnDrop(PointerEventData eventData)
    {
        if (!m_allowDrop) return;
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        draggableItem.parentAfterDrag = transform;
        
        if (transform.childCount != 0)
        {
            var child = transform.GetChild(0);
            child.transform.SetParent(draggableItem.parentBeforeDrag);
        }
    }
}
