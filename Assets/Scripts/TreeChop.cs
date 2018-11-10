using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChop : MonoBehaviour
{

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
            //StartCoroutine(destroyTree());
            isFallen = true;
        }

        if (treeHealth == 0 && isFallen == true)
        {

        }
    }

    private IEnumerator destroyTree()
    {
        yield return new WaitForSeconds(10);
        //Rigidbody rb = thisTree.GetComponent<Rigidbody>();
        //rb.isKinematic = true;
        //rb.velocity = Vector3.Slerp(rb.velocity, Vector3.zero, 0.2f);
        //rb.angularVelocity = Vector3.Slerp(rb.velocity, Vector3.zero, 0.2f);
        //Destroy(thisTree);
    }
}
