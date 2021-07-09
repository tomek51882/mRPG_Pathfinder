using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipInSlot equipSlot;
    public override void Use()
    {
        base.Use();
        Equip();

    }
    void Equip()
    {
        //EquipmentStorage storage = Resources.Load<EquipmentStorage>("Storage/EquipmentStorage");
        //RemoveFromInventory();
        //storage.Equip(this);
    }
    void Unequip()
    {
        
    }
}

public enum EquipInSlot { Head, Chest, Legs, Weapon, Feet, Hands }