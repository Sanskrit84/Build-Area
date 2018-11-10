using UnityEngine;

public class ItemPickup : MonoBehaviour

{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    [SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;

    private bool isInRange;

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(itemPickupKeyCode))
        {

            inventory.AddItem(item.GetCopy());
            Destroy(this.gameObject);
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
        }
    }

}
