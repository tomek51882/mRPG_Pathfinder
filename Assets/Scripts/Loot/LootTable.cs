using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LootTable : MonoBehaviour
{
    [SerializeField]
    public List<LootItem> items = new List<LootItem>();
}
