using UnityEngine;
using UnityEditor;
using System.Linq;

public class CollisionBuildingRule : MonoBehaviour, IBuildingRule
{
    private Builder builder;
    private Collider c; //A bounding box of chosen prefab

    public void AfterBuildEffect(GameObject newBuilding)
    {

    }

    public void Init(Builder b)
    {
        builder = b;
        c = builder.ghost.GetComponent<MeshCollider>();
    }

    public void Dispose() { }


    public bool IsRuleValid()
    {
        Bounds bounds = c.bounds;
        return !Physics.CheckBox(bounds.center, bounds.extents, builder.ghost.transform.rotation, 1024, QueryTriggerInteraction.Collide);
    }

}
