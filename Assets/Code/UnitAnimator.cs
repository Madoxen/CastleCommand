using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;


public class UnitAnimator : MonoBehaviour
{
    Animator animator;
    IDamageDealer dd;
    NavMeshAgent navAgent;
    Action<object> onAttack;
    
    // Use this for initialization
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        dd = GetComponentInChildren<IDamageDealer>();
        navAgent = GetComponent<NavMeshAgent>();

        

        if (dd != null)
        {
            onAttack = (sender) => animator.SetTrigger("Attacked");
            dd.Attacked += onAttack;
        }
    }

    private void FixedUpdate()
    {
        animator.SetFloat("MovementSpeed%", navAgent.velocity.magnitude / navAgent.speed);
    }

    private void OnDestroy()
    {
        dd.Attacked -= onAttack;
    }
}
