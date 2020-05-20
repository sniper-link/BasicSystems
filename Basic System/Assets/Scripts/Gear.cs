using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GearType{
    Bow,
    Sword,
    Shield,
    None
};

public enum AmmoType
{
    Arrow,
    Bomb,
}


//[CreateAssetMenu(fileName = "noNameGear", menuName = "Gear")]
public struct Gear //: ScriptableObject
{
    public string gearName;
    public GearType gearType;
    public int gearLevel;
    public bool needAmmo;
    public AmmoType ammoType;
    public GameObject gearBase;
    public bool canDamage;
    public float damage;
    public bool canCrit;
    public Image gearIcon;

    /*public Gear()
    {
        gearName = "noName";
        gearType = GearType.None;
        canDamage = false;
        damage = 0;
        canCrit = false;
        itemIcon = null;
    }

    public Gear(string name)
    {
        gearName = name;
        gearType = GearType.None;
        canDamage = false;
        damage = 0;
        canCrit = false;
        itemIcon = null;
    }

    public Gear(string name, GearType type, int level)
    {
        gearName = name;
        gearLevel = level;
        gearType = type;
        canDamage = false;
        damage = 0;
        canCrit = false;
        itemIcon = null;
    }*/

}
