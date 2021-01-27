using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// Represents object that is targatable by ITargetAcquisition
/// </summary>
public interface ITargetable : ITeamable
{
    event Action<ITargetable> TargetNoLongerValid; //Called when object enters a state where it's no longer targettable
    //ASK: Is this really the best way to represent this interface? It's called ITargetable but it can be in state where it's not ITargettable
}

