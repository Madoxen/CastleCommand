using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
//Selects a single target in range of trigger
public class SingleTargetAcquisition : MonoBehaviour
{

    private GameObject currentAttackTarget = null;
    public GameObject CurrentAttackTarget
    {
        get { return currentAttackTarget; }
        set { currentAttackTarget = value; }
    }





    private void OnTriggerStay(Collider other)
    {
        if (CurrentAttackTarget != null)
            return;

        if (other.gameObject.GetComponent<Enemy>() && other.gameObject.GetComponent<HealthComponent>()) //This might be performance taxing
        {
            CurrentAttackTarget = other.gameObject;
        }
    }


}
