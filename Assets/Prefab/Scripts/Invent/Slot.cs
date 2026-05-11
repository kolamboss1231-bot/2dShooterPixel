using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Slot : MonoBehaviour
{
    private Inventory Inventory;
    public int i;

    void Awake()
    {
    Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();   
    }

    void Update()
    {
        if(transform.childCount <=0)
        {
            Inventory.isFull[i] = false;        
        }
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
