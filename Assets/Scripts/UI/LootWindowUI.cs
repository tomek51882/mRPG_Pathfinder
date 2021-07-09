using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootWindowUI : MonoBehaviour
{
    [SerializeField] Transform itemsParent;
    public LootSlot[] lootSlots;
    public List<Item> droppedItems = new List<Item>();
    InventoryStorage storage;
    public delegate void OnWindowClose();
    public OnWindowClose OnWindowCloseCall;
    private void Start()
    {
        storage = Resources.Load<InventoryStorage>("InventoryStorage");

        lootSlots = itemsParent.GetComponentsInChildren<LootSlot>();
        for (int i = 0; i < lootSlots.Length; i++)
        {
            lootSlots[i].slotID = i;
            lootSlots[i].onSlotLooted += OnSlotLooted;
            if (i < droppedItems.Count)
            {
                if (droppedItems[i] != null)
                {
                    lootSlots[i].FillSlot(droppedItems[i]);
                }

            }
        }
    }
    public void OnSlotLooted(int slotIndex)
    {
        Item item = (Item)lootSlots[slotIndex].item;
        if (storage.AddItemToInventory(item))
        {
            lootSlots[slotIndex].ClearSlot();
            droppedItems[slotIndex] = null;
        }

        if (droppedItems.All(x => x == null))
        {
            CloseWindow();
        }
    }
    public void LootAll()
    {
        for (int i = 0; i < droppedItems.Count; i++)
        {
            Item item = (Item)lootSlots[i].item;
            if (!storage.AddItemToInventory(item))
            {
                Debug.LogError("Inventory is full!");
                return;
            }
            lootSlots[i].ClearSlot();
            droppedItems[i] = null;
        }
        CloseWindow();
    }
    public void CloseWindow()
    {
        //Destroy(this.gameObject);
        if (OnWindowCloseCall != null)
        {
            OnWindowCloseCall.Invoke();
        }
    }
}
