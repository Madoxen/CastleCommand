using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class EnemyBasicMeleeAttack : MonoBehaviour
{
    [SerializeField]
    private int damage;
    public int Damage
    {
        get { return damage; }
    }

    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }

    [SerializeField]
    private GameObject currentAttackTarget = null;
    public GameObject CurrentAttackTarget
    {
        get { return currentAttackTarget; }
        set { currentAttackTarget = value; }
    }

    private float currentAttackCooldown = 0f;

    private void FixedUpdate()
    {
        if (CurrentAttackTarget != null) //This might be performance taxing since we can have dozens of enemies on screen
        {
            currentAttackCooldown -= Time.fixedDeltaTime;
            if (currentAttackCooldown <= 0)
            {
                Attack();
                currentAttackCooldown = AttackSpeed;
            }
        }
    }

    private void Attack()
    {
        CurrentAttackTarget.GetComponent<HealthComponent>().CurrentHealth -= Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Building>() && other.gameObject.GetComponent<HealthComponent>()) //This might be performance taxing
        {
            if (currentAttackTarget == null)
            {
                currentAttackTarget = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentAttackTarget) 
        {
            currentAttackTarget = null;
        }
    }

}
