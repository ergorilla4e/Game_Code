using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickedup : MonoBehaviour
{
    public Item item = new Item("");
    private bool added = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            added = Inventory.Instance.AddItem(item.name);

            if (added)
            {
                Destroy(gameObject);
            }
        }
    }
}
