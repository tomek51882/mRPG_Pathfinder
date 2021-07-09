using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public BaseItem item;
    public Image icon;
    public int slotID;
    public void FillSlot(BaseItem newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseSlot()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
