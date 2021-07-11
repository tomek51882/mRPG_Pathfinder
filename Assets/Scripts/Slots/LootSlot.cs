using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSlot : Slot
{
    public delegate void OnSlotLootedDelegate(int slotIndex);
    public OnSlotLootedDelegate onSlotLooted;

    public void LootItem()
    {
        //Debug.Log("Clicked Loot0");
        if (item != null)
        {
            if (onSlotLooted != null)
            {
                onSlotLooted.Invoke(slotID);
            }
        }
    }
}
