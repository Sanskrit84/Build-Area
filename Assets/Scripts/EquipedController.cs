using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedController : MonoBehaviour

{
    public GameObject equiped;
    private bool isEquiped = false;
    private bool isSwing = false;

    [SerializeField]
    RectTransform InvPanel;
    [SerializeField]
    RectTransform CharPanel;

    UnityStandardAssets.Characters.FirstPerson.MouseLook mouseLook;
    [SerializeField]
    UnityStandardAssets.Characters.FirstPerson.FirstPersonController FPC;


    // Use this for initialization
    void Start ()
    {
        FPC = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        mouseLook = FPC.GetMouseLook();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //FIXME
        //Case setup for equiping
        if (!equiped.activeSelf && Input.GetKeyDown(KeyCode.Alpha1))
        {
            isEquiped = true;
            equiped.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isEquiped = false;
            equiped.SetActive(false);
        }

        //Raycast
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        

        if (Physics.Raycast(transform.position, fwd, out hit, 2))
        {
            //Debug.Log(hit.collider.tag);
            //FIXME
            //change to case for all interactable objects
            if (hit.collider.tag == "tree" && Input.GetMouseButtonDown(0) && isEquiped == true && isSwing == false)
            {
                equiped.GetComponent<Animation>().Play("swing");
                isSwing = true;
                StartCoroutine(swing());
                TreeChop treeScript = hit.collider.gameObject.GetComponent<TreeChop>();
                if (treeScript.treeHealth > 0)
                {
                    treeScript.treeHealth--;
                }
                else if (treeScript.treeHealth > 0 && treeScript.LogCount >= 0)
                {
                    treeScript.LogCount--;
                }
            }
        }


        //if (Input.GetKeyUp(KeyCode.I) && !InvPanel.gameObject.activeSelf)
        //{
        //    Debug.Log("I pressed and Inv not open");
        //    InvPanel.gameObject.SetActive(true);
        //    if(mouseLook.lockCursor)
        //    {
        //        unlockMouselook();
        //    }
        //}
        //else if(Input.GetKeyUp(KeyCode.I) && InvPanel.gameObject.activeSelf)
        //{
        //    //Debug.Log("I pressed and Inv was open");
        //    InvPanel.gameObject.SetActive(false);
        //    if(!CharPanel.gameObject.activeSelf)
        //    {
        //        lockMouselook();
        //    }
        //}

        //if (Input.GetKeyUp(KeyCode.C) && !CharPanel.gameObject.activeSelf)
        //{
        //    //Debug.Log("C pressed and Inv not open");
        //    CharPanel.gameObject.SetActive(true);
        //    if (mouseLook.lockCursor)
        //    {
        //        unlockMouselook();
        //    }
        //}
        //else if (Input.GetKeyUp(KeyCode.C) && CharPanel.gameObject.activeSelf)
        //{
        //    //Debug.Log("C pressed and Inv was open");
        //    CharPanel.gameObject.SetActive(false);
        //    if (!InvPanel.gameObject.activeSelf)
        //    {
        //        lockMouselook();
        //    }
        //}

    }

    //private void lockMouselook()
    //{
    //    mouseLook.lockCursor = true;
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    //}

    //private void unlockMouselook()
    //{
    //    //Debug.Log("Mouse locked, unlocking");
    //    mouseLook.lockCursor = false;
    //    Cursor.lockState = CursorLockMode.None;
    //    Cursor.visible = true;
    //}

    private IEnumerator swing()
    {
        yield return new WaitForSeconds(1);
        isSwing = false;
    }
}
