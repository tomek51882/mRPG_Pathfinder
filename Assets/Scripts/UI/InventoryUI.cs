using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    InventoryStorage storage;
    [SerializeField] Transform itemsParent;
    InventorySlot[] slots;
    public GameObject inventoryPanel;
    private void Awake()
    {
        storage = Resources.Load<InventoryStorage>("InventoryStorage");
        storage.onItemChanged += UpdateUI;
    }
    // Start is called before the first frame update
    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotID = i;
        }
        UpdateUI();
    }

    // Update is called once per frame
    public void OnToggleInventory(InputAction.CallbackContext value)
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
    void UpdateUI()
    {
        
    }
}
