using System.Collections;
using System.Collections.Generic;
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        parentBeforeDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
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
        }
        Invoke(nameof(TakeInfoMod), Time.deltaTime);

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

