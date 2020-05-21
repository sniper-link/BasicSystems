using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    static public PlayerInventory instance;
    public PlayerUI playerUI;
    public PlayerController playerController;

    //public Gear itemTest;

    public bool clearInventoryOnPlay;

    public GameObject pickupScrollBy;

    static public Gear inHandGear;

    static public Gear curSword;
    static public Gear curShield;
    static public Gear curBow;
    static public Gear curTool;

    string[] itemNameList =
    {
        "Gold", "Token",
        "Arrow", "Bomb",
        "Wood", "Rock", "Iron"
    };

    static private int goldAmount;
    static private Dictionary<string, Gear> storedGearBag;
    static private Dictionary<string, Item> storedItemBag;
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
        if (clearInventoryOnPlay)
        {
            gearBag.Clear();
            itemBag.Clear();
            keyItemBag.Clear();
            inHandGear = new Gear();
            curSword = new Gear();
            curShield = new Gear();
            curBow = new Gear();
            curTool = new Gear();
        }
        playerUI = gameObject.GetComponent<PlayerUI>();
        playerController = gameObject.GetComponent<PlayerController>();
    }
    
    void Start()
    {

    }

    private void OnEnable()
    {
        //Debug.Log("enabled");
    }

    private void OnDisable()
    {
        //Debug.Log("disabled");
    }

    private void PrintOutItems()
    {
        itemBag.TryGetValue("Money", out Item money);
        Debug.Log(money.amount);
    }

    public void AddToGearBag(Gear newGear)
    {
        if (gearBag.TryGetValue(newGear.gearType.ToString(), out Gear curGear))
        {
            //Debug.Log(curGear.gearName);
            if (newGear.gearLevel > curGear.gearLevel)
            {
                Debug.Log("Upgrade Gear To: " + newGear.gearName);
                gearBag[newGear.gearType.ToString()] = newGear;
                // get the gear gameobject
                //newGear.gameObject;
                UpdateGearBag(newGear);
            }
            else if (newGear.gearLevel == curGear.gearLevel)
            {
                Debug.Log("Same Gear Level " + newGear.gearName);
            }
            else if (newGear.gearLevel < curGear.gearLevel)
            {
                Debug.Log("Gear Level too low " + newGear.gearName);
            }
        }
        else
        {
            Debug.Log("Adding New Gear: " + newGear.gearName);
            gearBag.Add(newGear.gearType.ToString(), newGear);
            UpdateGearBag(newGear);
        }
    }

    public void AddToItemBag (Item newItem)
    {
        if (ValidateItem(newItem))
        {
            if(itemBag.TryGetValue(newItem.itemName, out Item curItem))
            {
                if (newItem.amount > 0 || newItem.amount < 0)
                {
                    curItem.amount += newItem.amount;
                }
                else if (newItem.amount == 0)
                {
                    Debug.Log("Please enter a non-zero amount");
                }
                itemBag[newItem.itemName] = curItem;
            }
            else
            {
                itemBag.Add(newItem.itemName, newItem);
            }
            Debug.Log("Added: " + newItem.itemName + " - " + newItem.amount);
            playerUI.PickupUpdate(newItem.itemIcon, newItem.amount);
        }
        else if (!ValidateItem(newItem))
        {
            Debug.Log("Please enter a valid item name: " + newItem.itemName);
        }
    }

    public void AddToKeyItemBag (KeyItem newKeyItem)
    {
        if (keyItemBag.TryGetValue(newKeyItem.keyItemName.ToString(), out KeyItem curKeyItme))
        {
            Debug.Log("WTF YOU HAVE A DUP");
        }
        else
        {
            keyItemBag.Add(newKeyItem.keyItemName, newKeyItem);
        }
    }

    public Gear GetFromGearBag(string gearType)
    {
        Gear testGear = new Gear();
        return testGear;
    }

    public Gear[] GetAllFromGearBag()
    {
        Gear[] PHgear = { };
        return PHgear;
    }

    public Item GetFromItemBag(string itemType, out bool haveItem)
    {
        haveItem = false;
        Item retrievedItem = new Item();
        if (itemBag.TryGetValue(itemType, out Item foundItem))
        {
            retrievedItem = foundItem;
            haveItem = true;
        }
        return retrievedItem;
    }

    public void GetFromKeyItemBag()
    {

    }

    public void UpdateItemBag(Item newItem)
    {
        if (ValidateItem(newItem))
        {
            itemBag[newItem.itemName] = newItem;
        }
        else if (!ValidateItem(newItem))
        {
            Debug.Log("Double Check Item Info");
        }
    }

    public void UpdateGearBag(Gear newGear)
    {
        if (newGear.gearType == GearType.Sword)
        {
            Debug.Log("attach gear");
            curSword = newGear;
            playerController.curSword = newGear;
            // attach to sword socket
            playerController.AttachGear(curSword);
        }
        else if (newGear.gearType == GearType.Shield)
        {

        }
        else if (newGear.gearType == GearType.Bow)
        {

        }
    }

    // probably never will be used, but just to have it, why not
    public void UpdateKeyItemBag(KeyItem newKeyItem)
    {

    }

    bool ValidateItem(Item checkItem)
    {
        bool returnBool = false;
        foreach (string validItemName in itemNameList)
        {
            if (checkItem.itemName == validItemName)
            {
                returnBool = true;
            }
        }
        return returnBool;
    }

    public void AddMoney(int amount)
    {
        if (itemBag.TryGetValue("Money", out Item money))
        {
            money.amount += amount;
        }
        /*else if (money == null)
        {
            itemBag.Add("Money", new Item("Money", ItemType.Currency));
            itemBag.TryGetValue("Money", out Item item);
            item.amount += amount;
        }*/
    }
}
