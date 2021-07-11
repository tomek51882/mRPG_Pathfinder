using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class NpcBase : MonoBehaviour
{
    public NpcData npcData;
    public string characterName;

    public virtual void DealDamage()
    {
        
    }    
}
