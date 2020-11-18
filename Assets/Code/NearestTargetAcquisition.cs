using UnityEngine;
using System.Collections;
using System;

public class NearestTargetAcquisition : MonoBehaviour, ITargetAcquisition
{
    public ITargetable CurrentAttackTarget => throw new NotImplementedException();
    public event Action<ITargetable> TargetAcquired;
}
