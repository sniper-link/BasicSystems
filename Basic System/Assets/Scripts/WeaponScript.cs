using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public BoxCollider hitBox;
    public SphereCollider pickupBox;
    public GameObject curPlayerOwner;
    public Pickup pickupScript;

    private bool pickedUp = false;

    private void Awake()
    {
        //Debug.Log("Weapon Script Awake State");
        if (pickupScript == null && gameObject.TryGetComponent<Pickup>(out Pickup curPickup))
        {
            pickupScript = curPickup;
        }
        hitBox.enabled = false;
        pickupBox.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (!pickedUp)
        {
            pickupBox.enabled = false;
            hitBox.enabled = true;
            pickedUp = true;
        } 
        else if (pickedUp)
        {
            Debug.Log("Hit " + other.name);
        }*/
        if (!other.CompareTag("Player"))
        {
            Debug.Log("hit something");
        }
    }

    private void OnEnable()
    {
        hitBox.enabled = true;
        pickupBox.enabled = false;
        if (!pickupScript)
        {
            if (pickupScript == null && gameObject.TryGetComponent<Pickup>(out Pickup curPickup))
            {
                pickupScript = curPickup;
            }
            curPlayerOwner = pickupScript.pickupOwner;
            //pickupScript.enabled = false;
            //Destroy(pickupScript);
        }
    }
    public void UseWeapon()
    {

    }
    
    public void SomethinElse()
    {

    }
}
