using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

//Leads enemy to the nearest building
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ITargetAcquisition))]
public class EnemyBasicNav : MonoBehaviour
{

    NavMeshAgent navAgent;
    Building target;
    ITargetAcquisition ac;

    // Start is called before the first frame update
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        ac = GetComponent<ITargetAcquisition>();
        ac.TargetAcquired += OnTargetAcquired;
    }

    private void OnTargetAcquired(ITargetable obj)
    {
        if(obj is MonoBehaviour target)
            navAgent.destination = target.transform.position;
    }
}
