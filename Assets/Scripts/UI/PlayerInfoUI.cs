using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    InventoryStorage inventoryStorage;
    public Slider healthBar;
    public Slider manaBar;
    public Text health;
    public Text mana;
    public Text characterName;
    public Text characterPanel_sta;
    public Text characterPanel_str;
    public Text characterPanel_agi;
    public Text characterPanel_int;
    // Start is called before the first frame update
    void Start()
    {
        inventoryStorage = Resources.Load<InventoryStorage>("InventoryStorage");
        inventoryStorage.onStatsChanged += UpdateUI;
        UpdateUI();
        //inventoryStorage.on
    }
    // Update is called once per frame
    //void Update()
    //{
        
    //}
    void UpdateUI()
    {
        healthBar.maxValue = inventoryStorage.maxHealth;
        manaBar.maxValue = inventoryStorage.maxMana;

        healthBar.value = inventoryStorage.currentHealth;
        manaBar.value = inventoryStorage.currentMana;

        health.text = inventoryStorage.currentHealth + "/" + inventoryStorage.maxHealth;
        mana.text = inventoryStorage.currentMana + "/" + inventoryStorage.maxMana;
        int staTotal = inventoryStorage.playerData.statistics.Stamina.GetTotalValue();
        int staBase = inventoryStorage.playerData.statistics.Stamina.GetBaseValue();
        int staBonus = inventoryStorage.playerData.statistics.Stamina.GetBonusValue();
        characterPanel_sta.text = "Stamina: " + staTotal + " (" + staBase + "+" + staBonus + ")";
    }
}
