﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using System;

public class Destroyer : MonoBehaviour
{
    Builder _builder;

    Camera camera;
    RaycastHit hit;
    int buildingsLayer;
    Builder builder
    {
        get {
            if (_builder is null)
            {
                _builder = GameObject.FindWithTag("Builder").GetComponent<Builder>();
            }
            return _builder;
        }
    }

    GameObject lastHit;
    Material lastMaterial;
    [SerializeField]
    Material destroyShader;

    MasterInput input;

    private void Awake()
    {
        input = MasterInputProvider.input;

        input.Destroyer.ConfirmDelete.performed += _ => OnConfirmDelete();
        input.Destroyer.CancelDelete.performed += _ => OnCancelDelete();
        input.Destroyer.MouseMove.performed += _ => OnMouseMove();

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
            if (hitObject != lastHit && hitObject.GetComponent<Building>()?.Team.TeamID == 0)
            {
                if (lastHit != null)
                    lastHit.GetComponent<Renderer>().material = lastMaterial;

                lastHit = hitObject;

                lastMaterial = hitObject.GetComponent<Renderer>().material;

                hitObject.GetComponent<Renderer>().material = destroyShader;
            }
        }
        else if (lastHit != null)
        {
            lastHit.GetComponent<Renderer>().material = lastMaterial;
            lastHit = null;
            lastMaterial = null;
        }
    }

    private void OnCancelDelete()
    {
        if (lastHit != null)
        {
            lastHit.GetComponent<Renderer>().material = lastMaterial;
            lastMaterial = null;
            lastHit = null;

        }
     
        gameObject.SetActive(false);
    }

    private void OnConfirmDelete()
    {
        if (lastHit != null)
        {
            Destroy(lastHit);
            lastMaterial = null;
        }
    }

    private void OnEnable()
    {
        builder.CurrentBuildingPrefab = null; //hope that's the right way to turn the builder off
        input.Destroyer.Enable();
    }

    private void OnDisable()
    {
        input.Destroyer.Disable();
    }
}
