using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        InventoryStorage storage = Resources.Load<InventoryStorage>("InventoryStorage");
        storage.Init();
    }
}
