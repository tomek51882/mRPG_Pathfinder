using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public RectTransform uiCanvas;
    public Image icon;
    private RectTransform rectTransform;
    private GameObject dragCopy;
    //InventoryStorage storage;
    void Awake()
    {
        //rectTransform = GetComponent<RectTransform>();
        if (GetComponent<Slot>() != null)
        {
            icon = GetComponent<Slot>().icon;
        }

    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        if (GetComponent<Slot>().item == null)
        {
            Debug.Log("Nothing to drag");
            eventData.pointerDrag = null;
            return;
        }
        dragCopy = GameObject.Instantiate(this.gameObject);
        dragCopy.transform.SetParent(uiCanvas);
        dragCopy.GetComponent<DraggableSlot>().enabled = false;
        dragCopy.AddComponent<CanvasGroup>().blocksRaycasts = false;
        rectTransform = dragCopy.GetComponent<RectTransform>();

        icon.color = new Color(0.5f, 0.5f, 0.5f);
    }
    public void OnDrag(PointerEventData eventData)
    {
        //window.GetComponent<RectTransform>().anchoredPosition = Mouse.current.position.ReadValue();
        if (eventData.pointerDrag != null)
        {
            rectTransform.anchoredPosition = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        icon.color = new Color(255, 255, 255);
        Destroy(dragCopy);
    }
}
