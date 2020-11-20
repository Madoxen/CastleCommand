using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;


public class UnitAnimator : MonoBehaviour
{
    Animator animator;
    IDamageDealer dd;

    Action<object> onAttack;

    // Use this for initialization
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        dd = GetComponent<IDamageDealer>();

        if (dd != null)
        {
            onAttack = (sender) => animator.SetTrigger("Attacked");
            dd.Attacked += onAttack;
        }
    }

    private void OnDestroy()
    {
        dd.Attacked -= onAttack;
    }
}
