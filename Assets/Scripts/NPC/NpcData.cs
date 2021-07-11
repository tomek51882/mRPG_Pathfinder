using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new NPC", menuName = "NPC/New NPC data")]
public class NpcData : ScriptableObject
{
    public string npcName;
    public Statistics statistics;
}
