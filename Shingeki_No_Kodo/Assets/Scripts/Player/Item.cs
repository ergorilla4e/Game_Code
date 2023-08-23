using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class Item
{
    public string name; 

    public Item(string name)
    {
        this.name = name;
    }
}
