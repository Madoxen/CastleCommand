using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(ITargetAcquisition))]
public class MeleeDamageDealer : MonoBehaviour, IDamageDealer
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
        if (targetAcquisition.CurrentAttackTarget == null)
            return;

        if (!(targetAcquisition.CurrentAttackTarget is MonoBehaviour target))
            return;

        //check if we are in range
        if (Vector3.Distance(target.transform.position, this.transform.position) < range)
        {
            target.GetComponent<HealthComponent>().CurrentHealth -= damage;
        }

        Attacked?.Invoke(this);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
