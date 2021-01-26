using System;

using UnityEngine;

public interface IProjectile
{
    void AfterHit(Collider col);
    Action<IProjectile, Collider> HitCallback { get; set; }
}