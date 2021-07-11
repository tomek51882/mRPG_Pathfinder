using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public BaseItem item;
    public Image icon;
    public int slotID;
    public virtual void FillSlot(BaseItem newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }
    public virtual void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseSlot()
    {
        if (item != null)
        {
            //Debug.Log("Using: " + item.name);
            item.Use();
        }
    }
}
