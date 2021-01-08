using UnityEngine;
using System.Collections;


public class PlaceOnNodeBuildingRule : MonoBehaviour, IBuildingRule
{
    private Builder builder;
    private new Collider collider; //A bounding box of chosen prefab

    [SerializeField]
    private StrategicResource requiredResource;

    public void AfterBuildEffect()
    {

    }

    public void Init(Builder b)
    {
        builder = b;
        collider = builder.ghost.GetComponent<MeshCollider>();
    }

    public void Dispose() { }


    public bool IsRuleValid()
    {
        Bounds bounds = collider.bounds;
        //2048 -> Resource nodes/fields layer
        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.extents, builder.ghost.transform.rotation, 2048, QueryTriggerInteraction.Collide); //might be taxing if called every frame? 
        //Find matching collider
        Debug.Log(colliders);
        foreach (Collider c in colliders)
        {
            Debug.Log(c.gameObject.name);
            StrategicResourceNode cmp = c.gameObject.GetComponent<StrategicResourceNode>();
            if (cmp != null)
                if (cmp.RepresentedResource == requiredResource)
                {
                    return true;
                }
        }

        return false;
    }

}
