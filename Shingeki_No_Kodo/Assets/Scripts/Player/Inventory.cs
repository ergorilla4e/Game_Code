using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public Action<Item> Action;

    public List<Item> items = new List<Item>();

    private int maxSlot = 3;

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

    public bool AddItem(string itemName)
    {
        GameObject itemPrefab = FindItemPrefabByName(itemName);

        if (itemPrefab == null)
        {
            //Debug.LogError("Prefab non trovato per l'oggetto: " + itemName);
            return false;
        }

        if (items.Count < maxSlot)
        {
            Item newItem = new Item(itemName); // Crea un nuovo oggetto Item
            items.Add(newItem);
            //Debug.Log(itemName + " è stato aggiunto all'inventario.");
            return true;
        }
        else
        {
            //Debug.Log("Non c'è spazio nell'inventario");
            return false;
        }
    }

    public GameObject FindItemPrefabByName(string itemName)
    {
        string cleanItemName = itemName.Replace("(Clone)", "");

        string path = "Prefab_BBT_UI/" + cleanItemName;

        GameObject itemPrefab = Resources.Load<GameObject>(path);

        if (itemPrefab == null)
        {
            //Debug.LogError("Prefab non trovato per l'oggetto: " + itemName);
            return null;
        }

        return itemPrefab;
    }

    public void RemoveItem(Item itemToRemove)
    {
        items.Remove(itemToRemove);
        Debug.Log(itemToRemove.name + " è stato rimosso dall'inventario.");
    }
}
