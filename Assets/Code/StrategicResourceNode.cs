using UnityEngine;
using System.Collections;
using System;


//World representation of resource;
public class StrategicResourceNode : MonoBehaviour
{
    [SerializeField]
    private StrategicResource representedResource;
    public StrategicResource RepresentedResource
    {
        get => representedResource;
    }


    private Building occupyingBuilding = null;
    public Building OccupyingBuilding
    {
        get { return occupyingBuilding; }
        set
        {
            if (occupyingBuilding != null)
                occupyingBuilding.WillBeDestroyed -= OnOccupyingBuildingDestroyed;
            occupyingBuilding = value;
            if (occupyingBuilding != null)
                occupyingBuilding.WillBeDestroyed += OnOccupyingBuildingDestroyed;
        }
    }

    //Used for properly setting building to null 
    private void OnOccupyingBuildingDestroyed(INotifyDestroy obj)
    {
        OccupyingBuilding = null;
    }
}
