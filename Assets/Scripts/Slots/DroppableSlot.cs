using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppableSlot : MonoBehaviour, IDropHandler
{

    InventoryStorage inventory;
    //public Slot slot;
    public void Awake()
    {
        inventory = Resources.Load<InventoryStorage>("InventoryStorage");
        //slot = GetComponent<Slot>();
        //Debug.Log(slot.GetType());
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("Something was dropped");
        }
    }
}
