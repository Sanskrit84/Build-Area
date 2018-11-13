using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChop : MonoBehaviour
{
    [SerializeField] GameObject logs;
    [SerializeField] GameObject invGroup;


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
        //Vector3 position = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        Vector3 position;
        float spacing;
        yield return new WaitForSeconds(7);
        for (int i = 0; i < LogCount; i++)
        {

            spacing = i * 2.1f;
            position = new Vector3(transform.position.x, (transform.position.y + spacing), transform.position.z);

            go.transform.SetParent(thisTree.transform);
            go.SetActive(false);
            go = (GameObject)Instantiate(logs, position, Quaternion.identity);

            ItemPickup itemPickup = go.GetComponent<ItemPickup>();
            //Inventory inventory = null;
                
            //itemPickup.GetInventory(inventory);
        }

        
        //Destroy(thisTree);
        go.SetActive(true);
    }
}
