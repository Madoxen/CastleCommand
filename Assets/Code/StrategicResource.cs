using UnityEngine;
using System.Collections;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StrategicResource", order = 1)]
public class StrategicResource : ScriptableObject
{
    public string Name;
    public string description;
    public Sprite icon;
}
