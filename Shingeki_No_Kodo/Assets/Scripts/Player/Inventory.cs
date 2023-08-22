using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public int coins = 50;


    public List <Slot> allSlots = new List<Slot>();

    public void RegisterSlot (Slot slot)
    {
        allSlots.Add (slot);    

    }
     public void UnregisterSlot (Slot slot)
    {
        allSlots.Remove(slot);
    }
}
