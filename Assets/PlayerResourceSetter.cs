using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerResourceSetter : MonoBehaviour
{
    public List<ResourceCost> Resources = new List<ResourceCost>();


    private void Awake()
    {
        Resources.ForEach(x=> PlayerResources.Instance.Resources.FirstOrDefault(y=>y.resource == x.resource).Amount = x.amount );
    }
}
