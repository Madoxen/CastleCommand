﻿using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(ITargetAcquisition))]
public class MeleeDamageDealer : MonoBehaviour, IDamageDealer, IDescriptorCreator
{
    public event Action<object> Attacked;
    public float range;
    public int damage;
    ITargetAcquisition targetAcquisition;


    private void Awake()
    {
        targetAcquisition = GetComponent<ITargetAcquisition>();
    }

    public void Attack()
    {

        if (!(targetAcquisition.CurrentAttackTarget is MonoBehaviour target))
            return;

        if (target != null)
        {
            //check if we are in range
            if (Vector3.Distance(target.transform.position, this.transform.position) < range)
            {
                target.GetComponent<HealthComponent>().CurrentHealth -= damage;
                Attacked?.Invoke(this);
            }
        }

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public Descriptor CreateDescription()
    {
        return new Descriptor
        {
            group = DescriptorGroup.STATS,
            priority = 1,
            text = "<Style=Stats>Damage: " + damage + "\nRange: " + range + "</style>"
        };
    }
}
