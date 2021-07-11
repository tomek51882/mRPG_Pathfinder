using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    // Start is called before the first frame update
    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }
}
