using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Money,
    Ammo,
    Material
}

[CreateAssetMenu(fileName = "noItemName", menuName = "Gear")]
public class Item : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public int amount;
}
