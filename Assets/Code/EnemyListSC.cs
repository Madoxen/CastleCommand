using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateEnemyListSC", order = 1)]
public class EnemyListSC : ScriptableObject
{
    public List<GameObject> prefabList = new List<GameObject>();
}
