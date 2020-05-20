using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    bool inRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<PlayerHealth>().TakeDamage(10);
            inRange = true;
            StartCoroutine(DamageOverTimer(2f, other.gameObject));
            //inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    IEnumerator DamageOverTimer (float timeBetweenDamage, GameObject player)
    {
        Debug.Log("Spike Damage");
        player.GetComponent<PlayerHealth>().TakeDamage(5);
        yield return new WaitForSeconds(timeBetweenDamage);
        if (inRange)
        {
            StartCoroutine(DamageOverTimer(2f, player));
        }
    }
}
