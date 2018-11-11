using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChop : MonoBehaviour
{
    [SerializeField] GameObject logs;
    [SerializeField] Inventory inventory;

    //Variables
    GameObject thisTree;
    public int LogCount = 4;
    public int treeHealth = 5;

    private bool isFallen = false;


    // Use this for initialization
    void Start()
    {
        thisTree = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (treeHealth <= 0 && isFallen == false)
        {
            Rigidbody rb = thisTree.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(Vector3.forward, ForceMode.Impulse);
            rb.mass = 300;
            StartCoroutine(destroyTree());
            isFallen = true;
        }

        if (treeHealth == 0 && isFallen == true)
        {

        }
    }

    private IEnumerator destroyTree()
    {
        GameObject go = new GameObject();
        Vector3 position;
        float spacing;
        //Quaternion direction = new Quaternion(transform.rotation.x, transform.localEulerAngles.y - 90, transform.localEulerAngles.z + 90, transform.rotation.w);

        for (int i = 0; i < LogCount; i++)
        {
            spacing = i * 2.1f;
            position = new Vector3(transform.position.x, (transform.position.y + spacing), transform.position.z);

            //GameObject go = (GameObject)Instantiate(logs, position, new Quaternion(transform.rotation.x, transform.localEulerAngles.y -90, transform.localEulerAngles.z + 90, transform.rotation.w));
            go.transform.parent = this.transform;
            go.SetActive(false);
            go = (GameObject)Instantiate(logs, position, transform.rotation);
            

            ItemPickup itemPickup = go.GetComponent<ItemPickup>();
            itemPickup.GetInventory(inventory);
        }
        //transform.rotation.x, transform.rotation.y - 90, transform.rotation.z + 90, transform.rotation.w
        yield return new WaitForSeconds(10);
        Destroy(thisTree);
        go.SetActive(true);
    }
}
