using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var otherItemTransform = eventData.pointerDrag.transform;
        if (transform.childCount != 0)
        {
            var parentT = otherItemTransform.parent;
        
            var child = transform.GetChild(0);
            child.transform.SetParent(parentT);
            child.localPosition = Vector3.zero;
        }
        
        otherItemTransform.SetParent(transform);
        otherItemTransform.localPosition = Vector3.zero;
    }
}
