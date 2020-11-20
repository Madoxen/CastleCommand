using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Represents components that provide target acquisition services
/// </summary>
public interface ITargetAcquisition
{
    ITargetable CurrentAttackTarget { get; } //Currently chosen attack target
    event Action<ITargetable> TargetAcquired; //Should called when CurrentAttackTarget changes
}
