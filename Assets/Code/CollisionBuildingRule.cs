﻿using UnityEngine;
using UnityEditor;


public class CollisionBuildingRule : MonoBehaviour, IBuildingRule
{
    private bool isValid = true;
    private Builder builder;

    public void AfterBuildEffect()
    {

    }

    public void Init(Builder b)
    {
        builder = b;
        builder.ghost.CollisionStayed += CollisionRegistered;
    }

    public void Dispose()
    {
        builder.ghost.CollisionStayed -= CollisionRegistered; //unregister
    }


    public bool IsRuleValid()
    {
        return true;
    }

    void CollisionRegistered(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            builder.ghost.IsValid = false;
        }
    }
}
