using UnityEngine;
using System.Collections;
using System;

public interface ITargetable
{
    bool IsValidTarget();
    event Action<ITargetable> TargetNoLongerValid;
    int Group { get; }
}

