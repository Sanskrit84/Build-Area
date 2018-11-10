using UnityEngine;

public class ItemChest : MonoBehaviour

{
    [SerializeField] Item item;
    [SerializeField] int amount = 1;
    [SerializeField] Inventory inventory;
    [SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;

    private bool isInRange;
    private bool isEmpty;


    private void OnValidate()
    {
        if (inventory == null)
            inventory = FindObjectOfType<Inventory>();

    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(itemPickupKeyCode))
        {
            Item itemCopy = item.GetCopy();
            if (inventory.AddItem(itemCopy))
            {
                amount--;
                if (amount == 0)
                {
                    isEmpty = true;
                }
            }
            else
            {
                itemCopy.Destroy();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

}
