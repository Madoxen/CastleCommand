using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateSpawnerList", order = 1)]
public class SpawnerListSC: ScriptableObject
{
    public List<GameObject> data = new List<GameObject>();
}
