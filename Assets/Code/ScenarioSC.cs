using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateScenarioSC", order = 1)]
public class ScenarioSC : ScriptableObject
{
    public List<int> Waves = new List<int>();
}
