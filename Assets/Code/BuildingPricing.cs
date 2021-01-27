using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[Serializable]
public class CostList
{
    public List<ResourceCost> Costs;
}


public class BuildingPricing : MonoBehaviour, IReadOnlyDictionary<string, CostList>
{
    private static BuildingPricing _instance;
    public static BuildingPricing Instance => _instance;

    public IEnumerable<string> Keys => BuildingNames;

    public IEnumerable<CostList> Values => Costs;

    public int Count => BuildingNames.Count;

    public CostList this[string key] => Costs[BuildingNames.IndexOf(key)];

    public List<string> BuildingNames = new List<string>();
    public List<CostList> Costs = new List<CostList>();

    public bool ContainsKey(string key) => BuildingNames.Contains(key);

    public bool TryGetValue(string key, out CostList value)
    {
        if (ContainsKey(key))
        { 
            value = this[key];
            return true;
        }
        value = null;
        return false;
    }

    public IEnumerator<KeyValuePair<string, CostList>> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogWarning("Singleton instance already registered");
        }
    }
}
