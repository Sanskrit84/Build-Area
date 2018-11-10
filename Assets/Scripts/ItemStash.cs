using UnityEngine;

public class ItemStash : ItemContainer

{
    [SerializeField] KeyCode openKeyCode = KeyCode.E;
    [SerializeField] Transform itemsParent;
    [SerializeField] UIInputController uIInputController;

    private bool isInRange;
    private bool isOpen;

    private Character character;


    protected override void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>(includeInactive: true);
    }

    protected override void Start()
    {
        base.Start();
        itemsParent.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(openKeyCode))
        {
            isOpen = !isOpen;
            //itemsParent.gameObject.SetActive(isOpen);

            if (isOpen)
            {
                uIInputController.ModifyUIElement(itemsParent.gameObject);
                character.OpenItemContainer(this);
            }
                
            else
            {
                uIInputController.ModifyUIElement(itemsParent.gameObject);
                character.CloseItemContainer(this);
            }
                

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other.gameObject, true);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckCollision(other.gameObject, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject, false);
    }

    private void CheckCollision(GameObject gameObject, bool state)
    {
        if (gameObject.CompareTag("Player"))
        {
            isInRange = state;

            if (!isInRange && isOpen)
                        {
                            isOpen = false;
                            itemsParent.gameObject.SetActive(false);
                            character.CloseItemContainer(this);
                        }
            if (isInRange)
            {
                
                character = gameObject.GetComponent<Character>();
            }

            else
                character = null;

        }
    }

}
