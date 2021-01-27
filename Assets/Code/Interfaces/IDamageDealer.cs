using UnityEngine;
using System.Collections;
using System;

//Represents components that are dealing damage
public interface IDamageDealer
{
    void Attack(); //Method that should deal damage or otherwise provide means to deal damage
    event Action<object> Attacked; //Event that is called when Attack is called and was successfull in its execution
}
