using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public enum PickupType
{
    // Gear Type
    Sword,
    Shield,
    Bow,

    // Key Item Type
    KeyItem,

    // Item Type
    Currency,
    Ammo,
    Material
}

//[ExecuteInEditMode]
public class Pickup : MonoBehaviour
{
    //public string itemName;
    //public ItemType itemType;
    public string pickupName;
    public PickupType pickupType;
    [Tooltip("If true, player will have to interact with item to pick it up." +
        " If false, player will pick up the item automatically")]
    public bool interactable;
    public bool shopItem;
    public bool showMeshAndMaterial;
    [HideInInspector, Tooltip("Gear level for the pickup item")]
    public int pickupGearLevel;
    [HideInInspector]
    public int pickupGearDamage;
    [HideInInspector]
    public bool pickupNeedAmmo;
    [HideInInspector]
    public AmmoType ammoType;
    [HideInInspector]
    public int amount;
    [HideInInspector]
    public string costItemName;
    [HideInInspector]
    public int costItemAmount;
    [HideInInspector]
    public Mesh itemMesh;
    [HideInInspector]
    public Material[] itemMaterial;
    [HideInInspector]
    public MeshRenderer testRender;
    [HideInInspector]
    public MeshCollider testCollider;
    [HideInInspector]
    public MeshFilter testFilter;
    public Sprite pickupImage;

    public GameObject pickupOwner;
    //public InputField damageField;

    //public ItemParam itemParam;

    private void Awake()
    {
        
        if (TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
        {
            testRender = renderer;
        }
        if (TryGetComponent<MeshCollider>(out MeshCollider collider))
        {
            testCollider = collider;
        }
        if (TryGetComponent<MeshFilter>(out MeshFilter filter))
        {
            testFilter = filter;
        }
    }

    private void Start()
    {
        if (interactable && shopItem)
        {
            Debug.Log("Pickup can't be both interactable and a shop item");
            shopItem = false;
        }
        if (testRender != null && itemMaterial != null && itemMaterial.Length > 0)
        {
            testRender.material = itemMaterial[0];
        }
        if (testCollider != null && itemMesh != null)
        {
            testCollider.sharedMesh = itemMesh;
        }
        if (testFilter != null && itemMesh != null)
        {
            testFilter.mesh = itemMesh;
        }
    }

    void OpenWeaponMenu()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.TryGetComponent<PlayerInventory>(out PlayerInventory curInv) 
            && other.TryGetComponent<PlayerController>(out PlayerController curCon)
            && !interactable)
        {
            pickupOwner = other.gameObject;
            AddToInventory(curCon, curInv);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<PlayerInventory>(out PlayerInventory curInv) 
            && other.TryGetComponent<PlayerController>(out PlayerController curCon)
            && other.TryGetComponent<PlayerUI>(out PlayerUI curUI)
            && interactable)
        {
            if (Input.GetKeyDown("f") && this.enabled)
            {
                Debug.Log("Picked up");
                AddToInventory(curCon, curInv);
                pickupOwner = other.gameObject;
                curUI.ShowInteractButton(false);
            }
            else if (!Input.GetKeyDown("f") && this.enabled)
            {
                curUI.ShowInteractButton(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<PlayerUI>(out PlayerUI curUI) && interactable)
        {
            curUI.ShowInteractButton(false);
        }
    }

    public void ShopItem(GameObject player)
    {
        if (player.TryGetComponent<PlayerInventory>(out PlayerInventory playerInv))
        {
            Item retrievedItem = playerInv.GetFromItemBag(costItemName, out bool haveItem);
            if (haveItem)
            {
                if (costItemAmount > retrievedItem.amount)
                {
                    // give prompt not enough amount to trade
                }
                else if (costItemAmount <= retrievedItem.amount)
                {
                    retrievedItem.amount -= costItemAmount;
                    playerInv.UpdateItemBag(retrievedItem);
                    Item newItem = new Item();
                    if (pickupType == PickupType.Ammo)
                    {
                        newItem.itemType = ItemType.Ammo;
                    }
                    else if (pickupType == PickupType.Currency)
                    {
                        newItem.itemType = ItemType.Currency;
                    }
                    else if (pickupType == PickupType.Material)
                    {
                        newItem.itemType = ItemType.Material;
                    }
                    newItem.amount = amount;
                    newItem.itemIcon = pickupImage;

                    playerInv.AddToItemBag(newItem);
                }
            }
        }
    }

    public void AddToInventory(PlayerController curCon, PlayerInventory curInv)
    {
        if (pickupType == PickupType.Sword || pickupType == PickupType.Shield || pickupType == PickupType.Bow)
        {
            // create gear struct 
            Gear newGear = new Gear();
            newGear.gearName = pickupName;
            if (pickupType == PickupType.Sword)
            {
                newGear.gearType = GearType.Sword;
            }
            else if (pickupType == PickupType.Shield)
            {
                newGear.gearType = GearType.Shield;
            }
            else if (pickupType == PickupType.Bow)
            {
                newGear.gearType = GearType.Bow;
            }
            newGear.gearLevel = pickupGearLevel;
            newGear.gearBase = this.gameObject;

            // add to inventory gearbag
            curInv.AddToGearBag(newGear);
            //WeaponScript curWeaponScript = gameObject.GetComponent<WeaponScript>();
            //curWeaponScript.enabled = true;
            
            
            this.enabled = false;
            //Destroy(this);
            //Destroy(gameObject);
        }
        else if (pickupType == PickupType.Ammo || pickupType == PickupType.Currency || pickupType == PickupType.Material)
        {
            // create item struct 
            Item newItem = new Item();
            newItem.itemName = pickupName;
            if (pickupType == PickupType.Ammo)
            {
                newItem.itemType = ItemType.Ammo;
            }
            else if (pickupType == PickupType.Currency)
            {
                newItem.itemType = ItemType.Currency;
            }
            else if (pickupType == PickupType.Material)
            {
                newItem.itemType = ItemType.Material;
            }
            newItem.amount = amount;
            newItem.itemIcon = pickupImage;

            // add to inventory itembag
            curInv.AddToItemBag(newItem);
            Destroy(gameObject);
        }
        else if (pickupType == PickupType.KeyItem)
        {
            // create key item struct
            KeyItem newKeyItem = new KeyItem();
            newKeyItem.keyItemName = pickupName;

            // add to inventory key item bag
            Destroy(gameObject);
        }
        
    }
}

[CustomEditor(typeof(Pickup))]
public class Pickup_Editor : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Pickup script = (Pickup)target;
        bool gearBool = script.pickupType == PickupType.Sword ? true : false;

        //script.pickupType = EditorGUILayout.Toggle(true, GUILayout.MinHeight(100));
        if (script.pickupType == PickupType.Sword || script.pickupType == PickupType.Shield || script.pickupType == PickupType.Bow)
        {
            //script.damageField = EditorGUILayout.ObjectField("Damage", script.damageField, typeof(InputField), true) as InputField;
            script.pickupGearLevel = EditorGUILayout.IntField("Pickup Gear Level", script.pickupGearLevel);
            script.pickupGearDamage = EditorGUILayout.IntField("Pickup Gear Damage", script.pickupGearDamage);
            script.pickupNeedAmmo = EditorGUILayout.Toggle("Pickup Need Ammo", script.pickupNeedAmmo);
            if (script.pickupNeedAmmo)
            {
                script.ammoType = (AmmoType)EditorGUILayout.EnumPopup("Ammo Type", script.ammoType);
            }
        }
        else if (script.pickupType == PickupType.Ammo || script.pickupType == PickupType.Material || script.pickupType == PickupType.Currency)
        {
            script.amount = EditorGUILayout.IntField("Amount", script.amount); 
        }
        else if (script.pickupType == PickupType.KeyItem)
        {

        }

        if (script.showMeshAndMaterial)
        {
            // add code for showing mesh and material

        }
        if (!script.showMeshAndMaterial)
        {

        }
    }
}

