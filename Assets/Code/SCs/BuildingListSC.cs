using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateBuildingListSC", order = 1)]
public class BuildingListSC : ScriptableObject
{
    public List<GameObject> prefabList = new List<GameObject>();
}
