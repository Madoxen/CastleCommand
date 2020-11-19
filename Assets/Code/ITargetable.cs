using UnityEngine;
using System.Collections;
using System;

public interface ITargetable : ITeamable
{
    bool IsValidTarget();
    event Action<ITargetable> TargetNoLongerValid;
}

