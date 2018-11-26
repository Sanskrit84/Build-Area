using UnityEngine;

public class ItemPickup : MonoBehaviour

{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    [SerializeField] WorkspaceInventory workspaceInventory;
    [SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;

    private bool isInRange;
    private bool inWorkspace;

    private void Start()
    {
        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
        }
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(itemPickupKeyCode))
        {
            inventory.AddItem(item.GetCopy());
            Destroy(this.gameObject);
            RemoveFromWorkspace();
            return;
        }

        if (isInRange && !inWorkspace)
        {
            //Add to Workpspace Inventory
            workspaceInventory.AddItem(item.GetCopy());
            inWorkspace = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            RemoveFromWorkspace();
        }
    }

    public void RemoveFromWorkspace()
    {
        //Remove from Workspace Inventory
        workspaceInventory.RemoveItem(item.GetCopy());
        inWorkspace = false;
    }


}
