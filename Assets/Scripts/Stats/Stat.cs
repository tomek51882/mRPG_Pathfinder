using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue = 0;//base character stats
    [SerializeField]
    private int bonusValue = 0;//bonus from equiped items
    public int actualBase = 0;
    public int actualBonus = 0;
    public List<StatModifier> statModifiers = new List<StatModifier>();//talents, buffs etc

    private float flatBonus = 0;
    private float percentStackBonus = 0f;
    private float percentTotalBonus = 0f;

    private void RecalculateBonuses()
    {
        float total = 0f;
        float bonus = 0f;
        float percentStack = 0f;

        statModifiers.Sort((StatModifier a, StatModifier b) => {
            if (a.type < b.type)
            {
                return -1;
            }
            else if (a.type > b.type)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        });

        foreach (StatModifier modifier in statModifiers)
        {
            if (modifier.type == ModifierType.FlatBonus)
            {
                bonus += modifier.value;
            }
            if (modifier.type == ModifierType.PercentStack)
            {
                percentStack += modifier.value;
            }
            if (modifier.type == ModifierType.PercentTotal)
            {
                total += modifier.value;
            }
        }
        flatBonus = bonus;
        percentStackBonus = percentStack/100;
        percentTotalBonus = total / 100;

        actualBase = Mathf.RoundToInt((baseValue * (1 + percentStackBonus)) * (1 + percentTotalBonus));
        actualBonus = Mathf.RoundToInt((bonusValue + flatBonus) * (1 + percentStackBonus) * (1 + percentTotalBonus));
    }
    public int GetRawBaseValue()
    {
        return baseValue;
    }
    public int GetRawBonusValue()
    {
        return bonusValue;
    }
    public int GetBaseValue()
    {
        return actualBase;
    }
    public int GetBonusValue()
    {
        return actualBonus;
    }
    public int GetTotalValue()
    {
        RecalculateBonuses();
        return actualBase + actualBonus;
    }
    public void SetBaseValue(int newValue)
    {
        baseValue = newValue;
        RecalculateBonuses();
    }
    public void SetBonusValue(int newValue)
    {
        bonusValue = newValue;
        RecalculateBonuses();
    }

}

/*
 
75  / 94
120 / 150
    / 246

 
 */