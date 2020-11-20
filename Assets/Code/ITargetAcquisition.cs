using UnityEngine;
using System.Collections;
using System;

public interface ITargetAcquisition
{
    ITargetable CurrentAttackTarget { get; }
    event Action<ITargetable> TargetAcquired;
}
