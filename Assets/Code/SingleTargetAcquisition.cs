using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(SphereCollider))]
//Selects a single target in range of trigger
public class SingleTargetAcquisition : MonoBehaviour, ITargetAcquisition
{

    private ITargetable currentAttackTarget = null;
    public ITargetable CurrentAttackTarget
    {
        get { return currentAttackTarget; }
        private set { currentAttackTarget = value; TargetAcquired?.Invoke(CurrentAttackTarget); }
    }

    private List<ITargetable> availableTargets = new List<ITargetable>();
    public event Action<ITargetable> TargetAcquired;

    private void OnTriggerEnter(Collider other)
    {
        ITargetable t = other.gameObject.GetComponent<ITargetable>();
        if (t != null)
        {
            availableTargets.Add(t);
            t.TargetNoLongerValid += OnCurrentTargetNoLongerValid;
            CurrentAttackTarget ??= t;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        availableTargets.Remove(other.GetComponent<ITargetable>());
    }

    private void OnCurrentTargetNoLongerValid(ITargetable sender)
    {
        sender.TargetNoLongerValid -= this.OnCurrentTargetNoLongerValid;
        availableTargets.Remove(sender);

        CurrentAttackTarget = availableTargets.Count > 0 ? availableTargets[0] : null;
    }

}
