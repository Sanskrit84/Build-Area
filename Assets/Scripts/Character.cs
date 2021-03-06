﻿using UnityEngine;
using UnityEngine.UI;
using Kryz.CharacterStats;
using System;

public class Character : MonoBehaviour
{

    [Header("Health Bar")]
    public CharacterStat Health;
    public CharacterStat Regeneration;
    public CharacterStat Resistance;
    public CharacterStat Vigour;

    [Header("Stamina Bar")]
    public CharacterStat Stamina;
    public CharacterStat Recovery;
    public CharacterStat Resilience;
    public CharacterStat Hardiness;

    [Header("Cognition Bar")]
    public CharacterStat Cognition;
    public CharacterStat Recall;
    public CharacterStat Focus;
    public CharacterStat Awareness;

    [Header("Morale Bar")]
    public CharacterStat Morale;
    public CharacterStat Courage;
    public CharacterStat Fortitude;
    public CharacterStat Determination;

    [Header("Basic Stats")]
    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligence;
    public CharacterStat Vitality;

    [SerializeField] Inventory inventory;
    [SerializeField] WorkspaceInventory workspaceInventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] CraftingWindow craftingWindow;
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Image draggableItem;
    [SerializeField] DropItemArea dropItemArea;
    [SerializeField] QuestionDialog questionDialog;
    [SerializeField] UIHUD uIHud;

    private BaseItemSlot dragItemSlot;

    private void Validate()
    {
        if (itemTooltip == null)
        {
            itemTooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    private void Start()
    {
        if (itemTooltip == null)
            itemTooltip = FindObjectOfType<ItemTooltip>();

        statPanel.SetStats(Health, Regeneration, Resistance, Vigour, Stamina, Recovery, Resilience, Hardiness, Cognition, Recall, Focus, Awareness, Morale, Courage, Fortitude, Determination, Strength, Agility, Intelligence, Vitality);
        statPanel.UpdateStatValues();

        uIHud.SetStats(Health, Regeneration, Resistance, Vigour, Stamina, Recovery, Resilience, Hardiness, Cognition, Recall, Focus, Awareness,  Morale, Courage, Fortitude, Determination);
        uIHud.UpdateStatValues();

        //Setup Events
        //Right Click
        inventory.OnRightClickEvent += InventoryRightClick;
        equipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;
        //Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;
        craftingWindow.OnPointerEnterEvent += ShowTooltip;
        workspaceInventory.OnPointerEnterEvent += ShowTooltip;

        //Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;
        craftingWindow.OnPointerExitEvent += HideTooltip;
        workspaceInventory.OnPointerExitEvent += HideTooltip;

        //Begin Drag
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;

        //End Drag
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;

        //Drag
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;

        //Drop
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
        dropItemArea.OnDropEvent += DropItemOutsideUI;
    }



    private void InventoryRightClick(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item is EquippableItem)
        {
            Equip((EquippableItem)itemSlot.Item);
            //uIHud.UpdateStatValues();
        }
        else if(itemSlot.Item is UsableItem)
        {
            UsableItem usableItem = (UsableItem)itemSlot.Item;
            usableItem.Use(this);
            statPanel.UpdateStatValues();

            if (usableItem.IsConsumable)
            {
                inventory.RemoveItem(usableItem);
                usableItem.Destroy();
                uIHud.UpdateStatValues();
            }
        }
    }

    private void EquipmentPanelRightClick(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item is EquippableItem)
        {
            Unequip((EquippableItem)itemSlot.Item);
        }
    }

    private void ShowTooltip(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            itemTooltip.ShowTooltip(itemSlot.Item);
        }
    }

    private void HideTooltip(BaseItemSlot itemSlot)
    {
        itemTooltip.HideTooltip();
    }

    private void BeginDrag(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(BaseItemSlot itemSlot)
    {
        dragItemSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(BaseItemSlot itemSlot)
    {
        if(draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }   
    }

    private void Drop(BaseItemSlot dropItemSlot)
    {
        if (dragItemSlot == null) return;

        if (dropItemSlot.CanAddStack(dragItemSlot.Item))
        {
            AddStacks(dropItemSlot);
        }
        else if (dropItemSlot.CanRecieveItem(dragItemSlot.Item) && dragItemSlot.CanRecieveItem(dropItemSlot.Item))
        {
            SwapItems(dropItemSlot);
        }
    }

    private void DropItemOutsideUI()
    {
        if (dragItemSlot == null) return;

        questionDialog.Show();
        BaseItemSlot baseItemSlot = dragItemSlot;
        questionDialog.OnYesEvent += () => DestroyItemInSlot(baseItemSlot);


    }

    private void DestroyItemInSlot(BaseItemSlot baseItemSlot)
    {
        baseItemSlot.Item.Destroy();
        baseItemSlot.Item = null;
    }

    private void SwapItems(BaseItemSlot dropItemSlot)
    {
        EquippableItem dragEquipItem = dragItemSlot.Item as EquippableItem;
        EquippableItem dropEquipItem = dropItemSlot.Item as EquippableItem;

        if (dropItemSlot is EquipmentSlot)
        {
            if (dragEquipItem != null) dragEquipItem.Equip(this);
            if (dropEquipItem != null) dropEquipItem.Unequip(this);
        }

        if (dragItemSlot is EquipmentSlot)
        {
            if (dragEquipItem != null) dragEquipItem.Unequip(this);
            if (dropEquipItem != null) dropEquipItem.Equip(this);
        }
        UpdateStatValues();

        Item draggedItem = dragItemSlot.Item;
        int draggedItemAmount = dragItemSlot.Amount;

        dragItemSlot.Item = dropItemSlot.Item;
        dragItemSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.Item = draggedItem;
        dropItemSlot.Amount = draggedItemAmount;
    }

    private void AddStacks(BaseItemSlot dropItemSlot)
    {
        int numAddableStacks = dropItemSlot.Item.MaximumStacks - dropItemSlot.Amount;
        int stacksToAdd = Mathf.Min(numAddableStacks, dragItemSlot.Amount);

        dropItemSlot.Amount += stacksToAdd;
        dragItemSlot.Amount -= stacksToAdd;
    }

    public void Equip(EquippableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null )
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    UpdateStatValues();
                }
                item.Equip(this);
                UpdateStatValues();
                //inventory.
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip(EquippableItem item)
    {
        if (!inventory.CanAddItem(item) && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            UpdateStatValues();
            inventory.AddItem(item);
        }
    }

    public void UpdateStatValues()
    {
        statPanel.UpdateStatValues();
        uIHud.UpdateStatValues();
    }

    private ItemContainer openItemContainer;

    private void TransferToItemContainer(BaseItemSlot itemSlot)
    {
        Item item = itemSlot.Item;
        if (item != null && openItemContainer.CanAddItem(item))
        {
            inventory.RemoveItem(item);
            openItemContainer.AddItem(item);
        }
    }

    private void TransferToInventory(BaseItemSlot itemSlot)
    {
        Item item = itemSlot.Item;
        if (item != null && inventory.CanAddItem(item))
        {
            openItemContainer.RemoveItem(item);
            inventory.AddItem(item);
        }
    }

    public void OpenItemContainer(ItemContainer itemContainer)
    {
        openItemContainer = itemContainer;

        inventory.OnRightClickEvent -= InventoryRightClick;
        inventory.OnRightClickEvent += TransferToItemContainer;

        itemContainer.OnRightClickEvent += TransferToInventory;

        itemContainer.OnPointerEnterEvent += ShowTooltip;
        itemContainer.OnPointerExitEvent += HideTooltip;
        itemContainer.OnBeginDragEvent += BeginDrag;
        itemContainer.OnEndDragEvent += EndDrag;
        itemContainer.OnDragEvent += Drag;
        itemContainer.OnDropEvent += Drop;


    }

    public void CloseItemContainer(ItemContainer itemContainer)
    {
        openItemContainer = null;

        inventory.OnRightClickEvent += InventoryRightClick;
        inventory.OnRightClickEvent -= TransferToItemContainer;

        itemContainer.OnRightClickEvent -= TransferToInventory;

        itemContainer.OnPointerEnterEvent -= ShowTooltip;
        itemContainer.OnPointerExitEvent -= HideTooltip;
        itemContainer.OnBeginDragEvent -= BeginDrag;
        itemContainer.OnEndDragEvent -= EndDrag;
        itemContainer.OnDragEvent -= Drag;
        itemContainer.OnDropEvent -= Drop;
    }

}
