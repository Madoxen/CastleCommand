using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
//Selects a single target in range of trigger
public class SingleTargetAcquisition : MonoBehaviour
{

    private ITargetable currentAttackTarget = null;
    public ITargetable CurrentAttackTarget
    {
        get { return currentAttackTarget; }
        private set { currentAttackTarget = value; }
    }

    private List<ITargetable> availableTargets = new List<ITargetable>();

    private void OnTriggerEnter(Collider other)
    {
        ITargetable t = other.gameObject.GetComponent<ITargetable>();
        if (t != null)
            availableTargets.Add(t);

    }

    private void OnTriggerExit(Collider other)
    {
        availableTargets.Remove(other.GetComponent<ITargetable>());
    }

    private void OnCurrentTargetNoLongerValid(ITargetable sender)
    {
        sender.TargetNoLongerValid -= this.OnCurrentTargetNoLongerValid;
        
    }

}
