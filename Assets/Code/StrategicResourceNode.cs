using UnityEngine;
using System.Collections;


//World representation of resource;
public class StrategicResourceNode : MonoBehaviour
{
    [SerializeField]
    private StrategicResource representedResource;
    public StrategicResource RepresentedResource
    {
        get => representedResource;
    }   
}
