using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    InventoryStorage storage;
    [SerializeField] Transform itemsParent;
    EquipmentSlot[] slots;
    private void Awake()
    {
        storage = Resources.Load<InventoryStorage>("InventoryStorage");
        storage.onEquipmentChanged += UpdateUI;
    }
    // Start is called before the first frame update
    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotID = i;
            slots[i].slotType = (EquipInSlot)i;
        }
        UpdateUI();
    }
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < storage.equipedItems.Count)
            {
                if (storage.equipedItems[i] != null)
                {
                    slots[i].FillSlot(storage.equipedItems[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }

            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
