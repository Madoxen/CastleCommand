using UnityEngine;
using System.Collections;
using System;


[RequireComponent(typeof(RegistryTargetAcquisition))]
public class MeleeDamageDealer : MonoBehaviour, IDamageDealer
{
    public event Action<object> Attacked;
    public float range;
    


    public void Attack()
    {
       
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
