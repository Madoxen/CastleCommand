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

    GameObject _lastHit;

    GameObject lastHit
    {
        get => _lastHit;
        set
        {
            if (value is null)
            {
                _lastHit = null;
                lastHitHC.HealthChangedEvent -= OnLastHitHealthChanged;
                //lastHitHC = null;
                lastHitRenderer.material = lastMaterial;
                //lastMaterial = null;
                //lastHitRenderer = null;

                return;
            }
            _lastHit = value;
            lastHitHC = _lastHit.GetComponent<HealthComponent>();
            lastHitRenderer = _lastHit.GetComponent<Renderer>();
            lastMaterial = lastHitRenderer.material;
            lastHitRenderer.material = repairShader;
            lastHitCL = BuildingPricing.Instance[lastHit.name.Split('(')[0]].Costs;
            lastHitHC.HealthChangedEvent += OnLastHitHealthChanged;
            UpdateTooltip(lastHitHC.CurrentHealth);
        }
    }

    private void OnLastHitHealthChanged(object sender, int currentHealth) => UpdateTooltip(currentHealth);

    private void UpdateTooltip(int currentHealth)
    {
        if (currentHealth <= 0)
        {
            Tooltip.HideTooltip();
            lastHit = null;
            return;
        }

        var resourceCosts = lastHitCL
                   .Select(rc => new ResourceCost { resource = rc.resource, amount = (int)(0.8 * rc.amount * (lastHitHC.MaxHealth - currentHealth) / lastHitHC.MaxHealth) }).ToList();

        var tooltipText = "Repair cost:\n";

        foreach (ResourceCost rc in resourceCosts)
        {
            StrategicResource r = rc.resource;
            tooltipText += "<sprite=\"GameIcons\" name=\"" + r.icon.name + "\"> : " + rc.amount + "\n";
        }

        Tooltip.ShowTooltip(tooltipText);
    }

    HealthComponent lastHitHC;
    Renderer lastHitRenderer;

    Material lastMaterial;
    [SerializeField]
    Material repairShader;

    MasterInput input;
    List<ResourceCost> lastHitCL;

    private void Awake()
    {
        input = MasterInputProvider.input;

        input.Reparator.ConfirmRepair.performed += _ => OnConfirmRepair();
        input.Reparator.CancelRepair.performed += _ => OnCancelRepair();
        input.Reparator.MouseMove.performed += _ => OnMouseMove();

        camera = Camera.main;
        buildingsLayer = LayerMask.GetMask("Buildings");

    }

    private void OnMouseMove()
    {
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingsLayer))
        {
            //building gets collored to show it is aimed at
            var hitObject = hit.collider.gameObject;

            if (hitObject != lastHit && hitObject.GetComponent<HealthComponent>() != null)
            {
                if (lastHit != null)
                    lastHit = null;

                lastHit = hitObject;

            }
        }
        else if (lastHit != null)
        {
            lastHit = null;
            Tooltip.HideTooltip();
        }
    }

    private void OnCancelRepair()
    {
        if (lastHit != null)
        {
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