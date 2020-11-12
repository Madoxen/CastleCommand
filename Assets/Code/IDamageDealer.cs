using UnityEngine;
using System.Collections;
using System;

public interface IDamageDealer
{
    void Attack();
    event Action<object> Attacked;
}
