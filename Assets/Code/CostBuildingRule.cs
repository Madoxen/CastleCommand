using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Assets.Code
{
    public class CostBuildingRule : MonoBehaviour, IBuildingRule
    {
        public List<ResourceCost> resourceCosts = new List<ResourceCost>();

        public void AfterBuildEffect()
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
    }

    [Serializable]
    public class ResourceCost
    {
        public StrategicResource resource;
        public int amount;
    }
}