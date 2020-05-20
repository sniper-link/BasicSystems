using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth = 100;
    public float curHealth = 100;
    public bool canRecovery;
    public Slider healthBar;

    private void Awake()
    {
        if (healthBar == null)
        {
            Debug.Log("Need Health Bar UI");
        }
    }

    private void Start()
    {
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateHealthBar()
    {
        healthBar.value = curHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if ((curHealth -= damage)  <= 0)
        {
            Debug.Log("player is dead");
            curHealth = 0f;
        }
        UpdateHealthBar();
    }
}
