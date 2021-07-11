using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryStorage", menuName = "Inventory/Inventory Storage Asset")]
public class InventoryStorage : ScriptableObject
{
    public NpcData playerData;
    public int playerLevel;
    public int experience;
    public int experienceToLevel;

    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;

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

    public delegate void OnStatsChangedDelegate();
    public OnStatsChangedDelegate onStatsChanged;

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
        if (playerData.statistics.Stamina.GetTotalValue()==0)
        {
            Debug.Log("Initializing Character Statistics");
            playerData.statistics.Stamina.SetBaseValue(10);
            playerData.statistics.Strength.SetBaseValue(5);
            playerData.statistics.Agility.SetBaseValue(5);
            playerData.statistics.Intellect.SetBaseValue(5);
            playerLevel = 1;
            experienceToLevel = 1000;
            currentHealth = maxHealth = playerData.statistics.Stamina.GetTotalValue() * 10;
            currentMana = maxMana = playerData.statistics.Intellect.GetTotalValue() * 12;
        }

        RecalculateStats();

        if (maxHealth != playerData.statistics.Stamina.GetTotalValue() * 10)
        {
            Debug.LogWarning("Invalid health value detected, recalculating stats");
            currentHealth = maxHealth = playerData.statistics.Stamina.GetTotalValue() * 10;
            currentMana = maxMana = playerData.statistics.Intellect.GetTotalValue() * 12;
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
        RemoveItemFromInventory(item);
        int slotIndex = (int)item.equipSlot;
        //Debug.Log("Request for equipping " + item.name);
        if (equipedItems[slotIndex] != null)
        {
            UnequipItem(slotIndex);
            //InventoryStorage inventory = Resources.Load<InventoryStorage>("Inventory/InventoryStorage");
            //inventory.AddItem(oldItem);
        }
        
        equipedItems[slotIndex] = item;
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke();
        }
        ApplyItemStats(item.statistics);
    }
    public void UnequipItem(int slotIndex)
    {
        Equipment oldItem = equipedItems[slotIndex];
        AddItemToInventory(oldItem);
        equipedItems[slotIndex] = null;
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke();
        }
        RemoveItemStats(oldItem.statistics);
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
    #region Stats
    public void RecalculateStats()
    {
        RemoveAllItemBonuses();
        foreach (Equipment item in equipedItems)
        {
            if (item != null)
            {
                ApplyItemStats(item.statistics);
            }

        }
    }
    public void RemoveAllItemBonuses()
    {
        playerData.statistics.Stamina.SetBonusValue(0);
        playerData.statistics.Strength.SetBonusValue(0);
        playerData.statistics.Agility.SetBonusValue(0);
        playerData.statistics.Intellect.SetBonusValue(0);
        UpdateStats();
    }
    public void ApplyItemStats(ItemStat[] stats)
    {
        foreach (ItemStat stat in stats)
        {
            int newValue = 0;
            switch (stat.name)
            {
                case StatType.Stamina:
                    newValue = playerData.statistics.Stamina.GetRawBonusValue();
                    newValue += stat.value;
                    playerData.statistics.Stamina.SetBonusValue(newValue);
                    break;
                case StatType.Strength:
                    newValue = playerData.statistics.Strength.GetRawBonusValue();
                    newValue += stat.value;
                    playerData.statistics.Strength.SetBonusValue(newValue);
                    break;
                case StatType.Agility:
                    newValue = playerData.statistics.Agility.GetRawBonusValue();
                    newValue += stat.value;
                    playerData.statistics.Agility.SetBonusValue(newValue);
                    break;
                case StatType.Intellect:
                    newValue = playerData.statistics.Intellect.GetRawBonusValue();
                    newValue += stat.value;
                    playerData.statistics.Intellect.SetBonusValue(newValue);
                    break;
            }
        }
        UpdateStats();
    }
    public void RemoveItemStats(ItemStat[] stats)
    {
        foreach (ItemStat stat in stats)
        {
            int newValue = 0;
            switch (stat.name)
            {
                case StatType.Stamina:
                    newValue = playerData.statistics.Stamina.GetRawBonusValue();
                    newValue -= stat.value;
                    if (newValue < 0)
                    {
                        newValue = 0;
                    }
                    playerData.statistics.Stamina.SetBonusValue(newValue);
                    break;
                case StatType.Strength:
                    newValue = playerData.statistics.Strength.GetRawBonusValue();
                    newValue -= stat.value;
                    if (newValue < 0)
                    {
                        newValue = 0;
                    }
                    playerData.statistics.Strength.SetBonusValue(newValue);
                    break;
                case StatType.Agility:
                    newValue = playerData.statistics.Agility.GetRawBonusValue();
                    newValue -= stat.value;
                    if (newValue < 0)
                    {
                        newValue = 0;
                    }
                    playerData.statistics.Agility.SetBonusValue(newValue);
                    break;
                case StatType.Intellect:
                    newValue = playerData.statistics.Intellect.GetRawBonusValue();
                    newValue -= stat.value;
                    if (newValue < 0)
                    {
                        newValue = 0;
                    }
                    playerData.statistics.Intellect.SetBonusValue(newValue);
                    break;
            }
        }
        UpdateStats();
    }
    public void UpdateStats()
    {
        maxHealth = playerData.statistics.Stamina.GetTotalValue() * 10;
        maxMana = playerData.statistics.Intellect.GetTotalValue() * 12;
        if (onStatsChanged != null)
        {
            onStatsChanged.Invoke();
        }
    }
    #endregion
    #region Damage
    public void DealDamage(int value)
    {
        currentHealth -= value;
        //check if hp < 0 

        if (onStatsChanged != null)
        {
            onStatsChanged.Invoke();
        }
    }
    public void HealDamage(int value)
    {
        currentHealth += value;
        //check if hp > max hp 

        if (onStatsChanged != null)
        {
            onStatsChanged.Invoke();
        }
    }
    #endregion
}
