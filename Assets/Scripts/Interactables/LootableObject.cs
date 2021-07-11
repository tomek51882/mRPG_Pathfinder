using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LootTable))]
public class LootableObject : Interactable
{
    public float lootRadius = 3f;

    public LootMode lootRollMode;
    public int numberOfItemsToDrop;
    public List<Item> droppedItems = new List<Item>();
    public GameObject LootWindow;
    public RectTransform uiCanvas;

    [SerializeField]
    bool canBeLooted = true;
    private LootTable lootTable;
    private bool isLootRolled = false;
    private bool isLootWindowOpen = false;
    GameObject window;

    private void Awake()
    {
        lootTable = GetComponent<LootTable>();
    }

    private void Update()
    {
        if (isLootWindowOpen)
        {
            if (Vector3.Distance(this.transform.position, interactedBy.transform.position) > lootRadius)
            {
                CloseLootWindow();
            }
        }
    }
    public override void Interact()
    {
        base.Interact();
        if (canBeLooted)
        {
            if (!isLootRolled)
            {

                RollItems();
                isLootRolled = true;
            }
            OpenLootWindow();
        }
    }

    public void RollItems()
    {
        if (lootRollMode == LootMode.UseDropChance)
        {
            foreach (LootItem item in lootTable.items)
            {
                int roll = (int)Random.Range(0, 100);
                if (roll < item.dropChance)
                {
                    droppedItems.Add(item.item);
                }
            }
        }
        else if (lootRollMode == LootMode.Fixed)
        {
            while (droppedItems.Count < numberOfItemsToDrop)
            {
                int index = (int)Random.Range(0, lootTable.items.Count);
                droppedItems.Add(lootTable.items[index].item);
            }
        }
        else if (lootRollMode == LootMode.DropEverything)
        {
            foreach (LootItem item in lootTable.items)
            {
                droppedItems.Add(item.item);
            }
        }
    }

    public void OpenLootWindow()
    {
        if (window == null)
        {
            window = Instantiate(LootWindow);
            window.transform.SetParent(uiCanvas);
            window.GetComponent<RectTransform>().anchoredPosition = Mouse.current.position.ReadValue();
            window.GetComponent<LootWindowUI>().droppedItems = droppedItems;
            window.GetComponent<LootWindowUI>().OnWindowCloseCall += CloseLootWindow;
            isLootWindowOpen = true;
        }

    }
    public void CloseLootWindow()
    {
        Destroy(window);
        window = null;
        isLootWindowOpen = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lootRadius);
    }

}
//malborska 91b lajma

public enum LootMode { UseDropChance, Fixed, DropEverything }
