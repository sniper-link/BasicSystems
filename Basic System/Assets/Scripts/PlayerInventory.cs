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

    // Start is called before the first frame update
    void Start()
    {
        /*AddToGear(bow1);
        AddToGear(bow2);
        AddToGear(bow3);
        AddToGear(sword1);
        AddToGear(shield1);*/
        //Debug.Log(bow.gearType);
        //gearBag.Add(bow.gearType.ToString(), bow);
    }

    // Update is called once per frame

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
        /*foreach (KeyValuePair<string, Gear> entry in gearBag)
        {
            Debug.Log("Item " + entry.Key + " - name: " + entry.Value.gearName + " || type: " + entry.Value.gearType);
        }*/
        itemBag.TryGetValue("Money", out Item money);
        Debug.Log(money.amount);
    }

    public void AddToGear(Gear newGear)
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
                UpdateGear(newGear);
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
            UpdateGear(newGear);
        }
    }

    public void UpdateGear(Gear newGear)
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

    public void AddToItem (Item newItem)
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

    public void AddToKeyItem (KeyItem newKeyItem)
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

    public Gear GetAllFromGearBag()
    {
        return new Gear();
    }

    public Item GetFromItemBag(string itemType)
    {
        Item testItem = new Item();
        if (itemBag.TryGetValue(itemType, out Item foundItem))
        {
            testItem = foundItem;
        }
        return testItem;
    }

    public void GetFromKeyItemBag()
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
