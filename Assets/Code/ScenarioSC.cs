using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[Serializable]
public class Wave
{
    public int Budget;
    public int Time;

    public Wave(int budget, int time)
    {
        Budget = budget;
        Time = time;
    }
    public Wave(){}
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateScenarioSC", order = 1)]
public class ScenarioSC : ScriptableObject
{
    public List<Wave> Waves = new List<Wave>();
}
