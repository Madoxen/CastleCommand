using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


class CostBuildingRule : MonoBehaviour, IBuildingRule, IDescriptorCreator
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

    public Descriptor CreateDescription()
    {
        string t = "";
        foreach (ResourceCost rc in resourceCosts)
        {
            StrategicResource r = rc.resource;
            t += "<sprite=\"GameIcons\" name=\"" + r.icon.name + "\"> :" + rc.amount + "\n";
        }

        return new Descriptor
        {
            group = DescriptorGroup.COST,
            priority = 0,
            text = t,
        };
    }
}

[Serializable]
public class ResourceCost
{
    public StrategicResource resource;
    public int amount;
}
