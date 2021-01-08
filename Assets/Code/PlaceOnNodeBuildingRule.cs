using UnityEngine;
using System.Collections;
using System;

public class PlaceOnNodeBuildingRule : MonoBehaviour, IBuildingRule
{
    private Builder builder;
    private new Collider collider; //A bounding box of chosen prefab

    [SerializeField]
    private StrategicResource requiredResource;
    private StrategicResourceNode nodeToBuildAt = null;

    public void AfterBuildEffect(GameObject newBuilding)
    {
        if (nodeToBuildAt == null)
            throw new NullReferenceException("nodeToBuildAt cannot null");


        if (newBuilding.TryGetComponent(out Building b))
        {
            nodeToBuildAt.OccupyingBuilding = b;
        }
        else
        {
            throw new NullReferenceException("newBuilding does not have Building component!");
        }
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
        nodeToBuildAt = null;
        //2048 -> Resource nodes/fields layer
        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.extents, builder.ghost.transform.rotation, 2048, QueryTriggerInteraction.Collide);
        //might be taxing if called every frame? 
        //Find matching collider
        Debug.Log(colliders);
        foreach (Collider c in colliders)
        {
            Debug.Log(c.gameObject.name);
            StrategicResourceNode srn = c.gameObject.GetComponent<StrategicResourceNode>();
            if (srn != null)
                if (srn.RepresentedResource == requiredResource && srn.OccupyingBuilding == null)
                {
                    nodeToBuildAt = srn;
                    return true;
                }
        }

        return false;
    }

}
