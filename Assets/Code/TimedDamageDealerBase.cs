using UnityEngine;
using System.Collections;
using System;


//Triggers damage dealer upon timer completion 
public class TimedDamageDealerBase : MonoBehaviour
{
    [SerializeField]
    private float cooldown;
    public float Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }

    private float currentAttackCooldown = 0f;
    public IDamageDealer damageDealerComponent;


    private void Awake()
    {
        damageDealerComponent = GetComponent<IDamageDealer>();
    }

    private void FixedUpdate()
    {
        currentAttackCooldown -= Time.fixedDeltaTime;
        if (currentAttackCooldown <= 0f)
        {
            damageDealerComponent.Attack();
            currentAttackCooldown = Cooldown;
        }
    }
}
