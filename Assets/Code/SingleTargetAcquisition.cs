using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(SphereCollider))]
//Selects a single target in range of trigger
//TODO: This is basically synonymous with nearest target acquisition...
public class SingleTargetAcquisition : MonoBehaviour, ITargetAcquisition
{

    private ITargetable currentAttackTarget = null;
    public ITargetable CurrentAttackTarget
    {
        get { return currentAttackTarget; }
        private set { currentAttackTarget = value; TargetAcquired?.Invoke(CurrentAttackTarget); }
    }

    public float DetectionRadius
    {
        get { return sc.radius; }
    }

    private List<ITargetable> availableTargets = new List<ITargetable>();
    public event Action<ITargetable> TargetAcquired;
    public int targetTeamID = 0;
    private SphereCollider sc;

    private void Start()
    {
        sc = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ITargetable t = other.gameObject.GetComponent<ITargetable>();
        if (t != null && t.Team == TeamRegister.GetTeamByID(targetTeamID))
        {
            availableTargets.Add(t);
            t.TargetNoLongerValid += OnCurrentTargetNoLongerValid;
            CurrentAttackTarget = CurrentAttackTarget ?? t;
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
