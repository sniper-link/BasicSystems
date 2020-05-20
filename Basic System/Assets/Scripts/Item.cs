using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Currency,
    Ammo,
    Material
}

//[CreateAssetMenu(fileName = "noItemName", menuName = "Gear")]
public struct Item //: ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public int amount;
    public Sprite itemIcon;

}
