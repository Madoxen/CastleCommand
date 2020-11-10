using UnityEngine;
using System.Collections;
using System;


public class HealthComponent : MonoBehaviour
{
    private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; OnHealthChanged(); }
    }

    public delegate void HealthChangedHandler(object sender, int currentHealth);
    public event HealthChangedHandler HealthChangedEvent;

    void OnHealthChanged()
    {
        //Notify subscribers about change of health
        HealthChangedEvent(this, currentHealth);
        if (CurrentHealth <= 0)
            Destroy(this.gameObject);
    }
}
