using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LootItem
{
    [SerializeField]
    public Item item;
    [SerializeField]
    public int dropChance;
}
