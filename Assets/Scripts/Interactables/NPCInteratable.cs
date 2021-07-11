using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteratable : Interactable
{
    public override void Interact()
    {
        base.Interact();
        OnFocus();
    }

    public override void OnFocus()
    {
        base.OnFocus();
        Debug.Log("Set NPC as target");
    }
}
