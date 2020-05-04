using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    static public PlayerInventory instance;

    public Gear itemTest;
    
    static private int goldAmount;
    static private Dictionary<string, Gear> storedGearBag;
    static private Dictionary<string, Item> stroedItemBag;
    static private Dictionary<string, KeyItem> storedKeyItemBag;

    Dictionary<string, Gear> gearBag = new Dictionary<string, Gear>();
    Dictionary<string, Item> itemBag = new Dictionary<string, Item>();
    Dictionary<string, KeyItem> keyItemBag = new Dictionary<string, KeyItem>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Gear bow1 = new Gear("Arman's Bow", GearType.Bow, 3);
        Gear bow2 = new Gear("Lavan's Bow", GearType.Bow, 4);
        Gear bow3 = new Gear("Basic Bow", GearType.Bow, 1);
        Gear sword1 = new Gear("Basic Sword", GearType.Sword, 1);
        Gear shield1 = new Gear("Basic Shield", GearType.Shield, 1);
        AddToGear(bow1);
        AddToGear(bow2);
        AddToGear(bow3);
        AddToGear(sword1);
        AddToGear(shield1);
        //Debug.Log(bow.gearType);
        //gearBag.Add(bow.gearType.ToString(), bow);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("v"))
        {
            if (gearBag.TryGetValue("Bow", out Gear curItem))
            {
                //var type = curItem.gearType;
                //Debug.Log("Cur Item: " + curItem.itemName + " " + curItem.gearType.ToString());
                PrintOutItems();
            }
        }
    }

    private void PrintOutItems()
    {
        foreach (KeyValuePair<string, Gear> entry in gearBag)
        {
            Debug.Log("Item " + entry.Key + " - name: " + entry.Value.gearName + " || type: " + entry.Value.gearType);
        }
    }

    private void AddToGear(Gear gear)
    {
        if (gearBag.TryGetValue(gear.gearType.ToString(), out Gear curGear) && gear.gearLevel > curGear.gearLevel)
        {
            Debug.Log("Upgrade Gear");
            gearBag.Add(gear.gearType.ToString(), gear);
        }
        else if (curGear == null)
        {
            Debug.Log("Add new Gear");
            gearBag.Add(gear.gearType.ToString(), gear);
        }
        else if (curGear != null)
        {
            Debug.Log("Gear too low score");
        }
        
    }
}
