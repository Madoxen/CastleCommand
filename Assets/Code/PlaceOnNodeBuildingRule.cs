using UnityEngine;
using System.Collections;
using System;
using UnityEngine.InputSystem;

public class PlaceOnNodeBuildingRule : MonoBehaviour, IBuildingRule
{
    private Builder builder;
    private new Collider collider; //A bounding box of chosen prefab

    [SerializeField]
    private StrategicResource requiredResource;
    private StrategicResourceNode nodeToBuildAt = null;

    private void Awake()
    {
    }


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

    public void Dispose() {
        builder.ghost.moveable = true;
    }


    public bool IsRuleValid()
    {
        Bounds bounds = collider.bounds;
        nodeToBuildAt = null;
        //2048 -> Resource nodes/fields layer
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 2048))
        {
            StrategicResourceNode srn = hit.collider.gameObject.GetComponent<StrategicResourceNode>();
            if (srn != null)
                if (srn.RepresentedResource == requiredResource && srn.OccupyingBuilding == null)
                {
                    builder.ghost.moveable = false;
                    nodeToBuildAt = srn;
                    builder.ghost.transform.position = new Vector3(srn.transform.position.x,
                        srn.transform.position.y,
                        srn.transform.position.z);
                    return true;
                }
        }
        else
        {
            builder.ghost.moveable = true;
        }
        return false;
    }

}
