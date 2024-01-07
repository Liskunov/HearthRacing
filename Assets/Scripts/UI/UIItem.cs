using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
   private RectTransform _rectTransform;
   private Canvas _mainCanvas;
   private CanvasGroup _canvasGroup;

   private void Awake()
   {
      _rectTransform = GetComponent<RectTransform>();
      _mainCanvas = GetComponentInParent<Canvas>();
      _canvasGroup = GetComponent<CanvasGroup>();
   }

   public void OnDrag(PointerEventData eventData)
   {
      _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
   }

   public void OnBeginDrag(PointerEventData eventData)
   {
      var slotTransform = _rectTransform.parent;
      slotTransform.SetAsLastSibling();
      _canvasGroup.blocksRaycasts = false;
   }

   public void OnEndDrag(PointerEventData eventData)
   {
      transform.localPosition = Vector3.zero;
      _canvasGroup.blocksRaycasts = true;
      
   }
}
