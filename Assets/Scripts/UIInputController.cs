using UnityEngine;
using System.Collections.Generic;

public class UIInputController : MonoBehaviour
{
    [SerializeField] private List<string> openUIElements = new List<string>();
    [SerializeField] UnityStandardAssets.Characters.FirstPerson.FirstPersonController FPC;
    [Space]
    [SerializeField] GameObject inventoryGameObject;
    [SerializeField] GameObject CharacterPanelGameObject;
    [SerializeField] GameObject CraftingPanelGameObject;
    [SerializeField] KeyCode[] toggleInventoryKeys;
    [SerializeField] KeyCode[] toggleCharacterPanelKeys;
    [SerializeField] KeyCode[] toggleCraftingPanelKeys;
    [Space]
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] StatTooltip statTooltip;


    UnityStandardAssets.Characters.FirstPerson.MouseLook mouseLook;

    private void Start()
    {
        mouseLook = FPC.GetMouseLook();
    }


    void Update()
    {
        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                ModifyUIElement(inventoryGameObject.transform.GetChild(0).gameObject);
                break;
            }
        }


        for (int i = 0; i < toggleCharacterPanelKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleCharacterPanelKeys[i]))
            {
                ModifyUIElement(CharacterPanelGameObject);
                break;
            }
        }

        for (int i = 0; i < toggleCraftingPanelKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleCraftingPanelKeys[i]))
            {
                ModifyUIElement(CraftingPanelGameObject);
                break;
            }
        }
    }


    public void ModifyUIElement(GameObject uIElement)
    {
        if (uIElement.activeSelf)
        {
            uIElement.SetActive(false);
            openUIElements.Remove(uIElement.transform.name);
            //Close open tooltips when window closes?
            //itemTooltip.HideTooltip();
            //statTooltip.HideTooltip();

            if (openUIElements.Count == 0)
            {
                HideMouseCursor();
            }
        }
        else
        {
            ShowMouseCursor();
            uIElement.SetActive(true);
            openUIElements.Add(uIElement.transform.name);
        }
    }

    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mouseLook.lockCursor = false;
    }

    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook.lockCursor = true;
    }
}
