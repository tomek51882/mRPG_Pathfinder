using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(NPCInteratable))]
public class Npc : NpcBase
{
    public int maxHealth;
    public int currentHealt;

    void Start()
    {
        Debug.Log(npcData.npcName);
        characterName = npcData.npcName;
        currentHealt = maxHealth = npcData.statistics.Stamina.GetTotalValue() * 10;
    }

    void Update()
    {
        
    }
    public override void DealDamage()
    {
        //base.DealDamage();
    }
}
