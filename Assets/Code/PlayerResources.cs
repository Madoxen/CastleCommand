using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerResources : MonoBehaviour
{

    private static PlayerResources instance = null;
    public static PlayerResources Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private StrategicResourceList resourceList;
    public List<ConcreteResource> Resources { get; } = new List<ConcreteResource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Singleton instance already registered");
        }


        foreach (StrategicResource r in resourceList.data)
        {
            Resources.Add(new ConcreteResource(r, 500)); //Create list of all resources that are in the game
        }
    }
}


