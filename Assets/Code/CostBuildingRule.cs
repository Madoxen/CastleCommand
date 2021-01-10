using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


public class CostBuildingRule : MonoBehaviour, IBuildingRule, ITooltipDescriptor
{
    public List<ResourceCost> resourceCosts = new List<ResourceCost>();
    private Builder builder;

    public void AfterBuildEffect(GameObject newBuilding)
    {
        foreach (ResourceCost c in resourceCosts)
        {
            ConcreteResource q = PlayerResources.Instance.Resources.First(x => x.resource == c.resource);
            q.Amount -= c.amount;
        }
    }

    public bool IsRuleValid()
    {
        foreach (ResourceCost c in resourceCosts)
        {
            ConcreteResource q = PlayerResources.Instance.Resources.FirstOrDefault(x => x.resource == c.resource);
            if (q == null || q.Amount < c.amount)
                return false;
        }

        return true;
    }

    public void Init(Builder b)
    {
        builder = b;
    }

    public void Dispose()
    {
    }

    public string CreateDescription()
    {
        string result = "";
        foreach (ResourceCost rc in resourceCosts)
        {
            StrategicResource r = rc.resource;
            result += "<sprite=\"GameIcons\" name=\"" + r.icon.name + "\"> :" + rc.amount + "\n";
        }
        return result;
    }
}

[Serializable]
public class ResourceCost
{
    public StrategicResource resource;
    public int amount;
}
