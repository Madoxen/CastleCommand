using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateResourceList", order = 1)]
public class StrategicResourceList : ScriptableObject
{
    public List<StrategicResource> data = new List<StrategicResource>();
}
