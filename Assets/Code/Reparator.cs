using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using System;
using System.Linq;

public class Reparator : MonoBehaviour
{
    Builder _builder;

    Camera camera;
    RaycastHit hit;
    int buildingsLayer;
    Builder builder
    {
        get
        {
            if (_builder is null)
            {
                _builder = GameObject.FindWithTag("Builder").GetComponent<Builder>();
            }
            return _builder;
        }
    }

    GameObject lastHit;
    Material lastMaterial;
    Material repairShader;

    MasterInput input;

    private void Awake()
    {
        input = new MasterInput();

        input.Reparator.ConfirmRepair.performed += _ => OnConfirmRepair();
        input.Reparator.CancelRepair.performed += _ => OnCancelRepair();
        input.Reparator.MouseMove.performed += _ => OnMouseMove();

        camera = Camera.main;
        buildingsLayer = LayerMask.GetMask("Buildings");
        repairShader = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Yellow.4");
    }

    private void OnMouseMove()
    {
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingsLayer))
        {
            //building gets collored to show it is aimed at
            var hitObject = hit.collider.gameObject;

            if (hitObject != lastHit)
            {
                if (lastHit != null)
                    lastHit.GetComponent<Renderer>().material = lastMaterial;

                lastHit = hitObject;

                lastMaterial = hitObject.GetComponent<Renderer>().material;

                hitObject.GetComponent<Renderer>().material = repairShader;
            }
        }
        else if (lastHit != null)
        {
            lastHit.GetComponent<Renderer>().material = lastMaterial;
            lastHit = null;
            lastMaterial = null;
        }
    }

    private void OnCancelRepair()
    {
        if (lastHit != null)
        {
            lastHit.GetComponent<Renderer>().material = lastMaterial;
            lastMaterial = null;
            lastHit = null;

        }

        gameObject.SetActive(false);
    }

    private void OnConfirmRepair()
    {
        if (lastHit != null)
        {
            var hc = lastHit.GetComponent<HealthComponent>();
            var costList = BuildingPricing.Instance[lastHit.name.Split('(')[0]];

            var resourceCost = costList.Costs.Select(rc => new ResourceCost { resource = rc.resource, amount = (int)(0.8 * rc.amount * (hc.MaxHealth - hc.CurrentHealth) / hc.MaxHealth) });

            if (resourceCost.All(rc => PlayerResources.Instance.Resources.Find(it => it.resource == rc.resource).Amount >= rc.amount))
            {
                foreach (var rc in resourceCost)
                {
                    PlayerResources.Instance.Resources.Find(it => it.resource == rc.resource).Amount -= rc.amount;
                    hc.CurrentHealth = hc.MaxHealth;
                }
            }
        }
    }

    private void OnEnable()
    {
        builder.CurrentBuildingPrefab = null; //hope that's the right way to turn the builder off
        input.Reparator.Enable();
    }

    private void OnDisable()
    {

        input.Reparator.Disable();
    }
}