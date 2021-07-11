using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStat
{
    public StatType name;
    public int value;
}
public enum StatType { Stamina, Strength, Agility, Intellect }

