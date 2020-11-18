using UnityEngine;
using System.Collections;
using System;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private int currentHealth = 1;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            OnHealthChanged();
        }
    }

    [SerializeField]
    private int maxHealth = 1;
    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            OnMaxHealthChanged();
        }
    }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public delegate void HealthChangedHandler(object sender, int currentHealth);
    public event HealthChangedHandler HealthChangedEvent;
    public event HealthChangedHandler MaxHealthChangedEvent;

    void OnHealthChanged()
    {
        //Notify subscribers about change of health
        HealthChangedEvent?.Invoke(this, currentHealth);
        if (CurrentHealth <= 0)
            Destroy(this.gameObject);
    }

    void OnMaxHealthChanged()
    {
        //Notify subscribers about change of max health
        MaxHealthChangedEvent?.Invoke(this, currentHealth);
    }


    private void OnDestroy()
    {
        //Unsubscribe everyone from events
        HealthChangedEvent = null;
        MaxHealthChangedEvent = null;
    }
}