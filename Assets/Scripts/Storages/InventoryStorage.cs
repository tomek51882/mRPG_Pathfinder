using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryStorage", menuName = "Inventory/Inventory Storage Asset")]
public class InventoryStorage : ScriptableObject
{
    public int inventorySize = 20;
    public List<Item> items = new List<Item>();
    public List<Equipment> equipedItems = new List<Equipment>();
    public List<Spell> learnedSpells = new List<Spell>();
    public List<Spell> equipedSpells = new List<Spell>();

    public delegate void OnItemChangedDelegate();
    public OnItemChangedDelegate onItemChanged;

    public delegate void OnEquipmentChangedDelegate();
    public OnEquipmentChangedDelegate onEquipmentChanged;

    public delegate void OnSpellLeanedDelegate();
    public OnSpellLeanedDelegate onSpellLearned;

    public delegate void OnSpellEquipDelegate();
    public OnSpellEquipDelegate onSpellEquip;

    public void Init()
    {

        if (items.Count < inventorySize)
        {
            Debug.Log("Initializing Inventory Storage");
            int slotsToCreate = inventorySize - items.Count;
            for (int i = 0; i < slotsToCreate; i++)
            {
                items.Add(null);
            }
        }
        if (equipedItems.Count < System.Enum.GetNames(typeof(EquipInSlot)).Length)
        {
            int slotsToCreate = System.Enum.GetNames(typeof(EquipInSlot)).Length  - equipedItems.Count;
            Debug.Log("Initializing Equipment Storage");
            for (int i = 0; i < slotsToCreate; i++)
            {
                equipedItems.Add(null);
            }
        }
        if (equipedSpells.Count < 10)
        {
            Debug.Log("Initializing Spell Storage");
            int slotsToCreate = 10 - equipedSpells.Count;
            for (int i = 0; i < slotsToCreate; i++)
            {
                equipedSpells.Add(null);
            }
        }
    }

    #region Inventory
    public bool AddItemToInventory(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                if (onItemChanged != null)
                {
                    onItemChanged.Invoke();
                }
                return true;
            }
        }
        Debug.LogError("Inventory is full!");
        return false;
    }
    public void RemoveItemFromInventory(Item item)
    {
        int index = items.IndexOf(item);
        items[index] = null;
        if (onItemChanged != null)
        {
            onItemChanged.Invoke();
        }
    }
    public void SwitchItemInSlots(int firstSlot, int secondSlot)
    {
        if (items[secondSlot] == null)
        {
            Item item = items[firstSlot];
            items[firstSlot] = null;
            items[secondSlot] = item;
        }
        else
        {
            Item firstItem = items[firstSlot];
            Item secondItem = items[secondSlot];
            items[firstSlot] = secondItem;
            items[secondSlot] = firstItem;
        }

        if (onItemChanged != null)
        {
            onItemChanged.Invoke();
        }
    }
    #endregion
    #region Equipment
    public void EquipItem(Equipment item)
    {
        
    }
    public void UnequipItem(int slotIndex)
    {
        
    }
    #endregion
    #region Spells
    public void LearnSpell(Spell spell)
    {
        learnedSpells.Add(spell);
        if (onSpellLearned != null)
        {
            onSpellLearned.Invoke();
        }
    }
    #endregion
}
