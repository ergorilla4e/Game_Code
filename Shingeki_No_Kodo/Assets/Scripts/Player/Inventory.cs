using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public Action<Item> Action;

    public List<Item> items = new List<Item>();

    private int maxSlot = 3;

    private GameObject[] supportObject;
    private Transform inventoryParent;

    private void Awake()
    {
        Action = RemoveItem;

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        supportObject = GameObject.FindGameObjectsWithTag("Inventario"); //Perché restituisce un array la funzione
        inventoryParent = supportObject[0].GetComponent<Transform>(); //Mi serve solo il transform per lavorare con i child
    }

    public bool AddItem(string itemName)
    {
        GameObject itemPrefab = FindItemPrefabByName(itemName);

        if (itemPrefab == null)
        {
            return false;
        }

        if (items.Count < maxSlot)
        {
            Item newItem = new Item(itemName); // Crea un nuovo oggetto Item
            items.Add(newItem);
            UpdateUIInventory();
            return true;
        }
        else
        {
            return false;
        }
    }

    //public void UpdateUIInventory()
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        Transform slotTransform = inventoryParent.GetChild(i);
    //
    //        if (i < Inventory.Instance.items.Count && Inventory.Instance.items[i] != null)
    //        {
    //            // Controlla se l'oggetto UI è già istanziato
    //            if (slotTransform.childCount == 0)
    //            {
    //                GameObject itemPrefab = Inventory.Instance.FindItemPrefabByName(Inventory.Instance.items[i].name);
    //
    //                if (itemPrefab != null)
    //                {
    //                    Instantiate(itemPrefab, slotTransform.position, Quaternion.identity, slotTransform);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (slotTransform.childCount > 0)
    //            {
    //                Destroy(slotTransform.GetChild(0).gameObject);
    //            }
    //        }
    //    }
    //}

    public void UpdateUIInventory()
    {
        // Controlla tutti gli slot dell'inventario
        for (int i = 0; i < 3; i++)
        {
            Transform slotTransform = inventoryParent.GetChild(i);

            // Rimuove tutti gli oggetti UI presenti nello slot
            foreach (Transform child in slotTransform)
            {
                Destroy(child.gameObject);
            }

            if (i < Inventory.Instance.items.Count && Inventory.Instance.items[i] != null)
            {
                GameObject itemPrefab = Inventory.Instance.FindItemPrefabByName(Inventory.Instance.items[i].name);

                if (itemPrefab != null)
                {
                    // Istanzia il nuovo oggetto UI nello slot
                    Instantiate(itemPrefab, slotTransform.position, Quaternion.identity, slotTransform);
                }
            }
        }
    }

    public GameObject FindItemPrefabByName(string itemName)
    {
        string cleanItemName = itemName.Replace("(Clone)", "");

        string path = "Prefab_BBT_UI/" + cleanItemName;

        GameObject itemPrefab = Resources.Load<GameObject>(path);

        if (itemPrefab == null)
        {
            return null;
        }

        return itemPrefab;
    }

    public void RemoveItem(Item itemToRemove)
    {
        items.Remove(itemToRemove);
        UpdateUIInventory();
        Debug.Log(itemToRemove.name + " è stato rimosso dall'inventario.");
    }
}
