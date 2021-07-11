using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNpcStatBridge : NpcBase
{
    InventoryStorage inventoryStorage;
    private void Start()
    {
        inventoryStorage = Resources.Load<InventoryStorage>("InventoryStorage");
    }
    public override void DealDamage()
    {
        //base.DealDamage();
    }
}
