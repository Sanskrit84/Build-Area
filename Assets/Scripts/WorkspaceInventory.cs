using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkspaceInventory : ItemContainer 
    {

    [SerializeField] Transform itemsParent;



    protected override void Start()
    {
        base.Start();
    }

    protected override void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>(includeInactive: true);
    }

}
