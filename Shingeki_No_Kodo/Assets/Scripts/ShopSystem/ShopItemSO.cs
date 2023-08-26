using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "shopmenu", menuName = "Scriptble Objects/New Shop Item", order = 1)] //crea la voce Scriptable Objects quando si cerca di creare un nuovo elemento.
public class ShopItemSO : ScriptableObject
{
    public string title;
    public string description;
    public int basecost;
    public Sprite spriteIcon;
}
