using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int i;

    //public event Action<GameObject> ItemDropped; //definiamo l'evento



    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        inventory.RegisterSlot(this);
    }

    private void Update()
    {
        if(transform.childCount <= 0 )
        {
            inventory.isFull[i] = false;
        }
    }

    public void DropItem()
    {
        foreach(Transform child in transform)
        {
            GameObject item = child.gameObject;
            //if (ItemDropped!=null)
            //{
            //   // ItemDropped.Invoke(item); //lanciamo l'evento quando l'oggetto viene elimnato dall'inventario
            //}
          
        }
       
    }

}
