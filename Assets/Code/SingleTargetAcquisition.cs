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

    private List<GameObject> availableAttackTargets = new List<GameObject>();
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() && other.gameObject.GetComponent<HealthComponent>()) //This might be performance taxing
        {
            if (currentAttackTarget == null)
            {
                CurrentAttackTarget = other.gameObject;
            }
            else
                availableAttackTargets.Add(other.gameObject);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentAttackTarget)
        {
            CurrentAttackTarget = null;
        }
    }

}
