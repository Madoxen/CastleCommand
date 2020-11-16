using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

//TODO: Extract this as IBuildingRule
public class BuildingGhost : MonoBehaviour
{
    public bool IsValid
    {
        get { return isValid; }
        private set
        {
            if (isValid == value)
                return;

            isValid = value;
            if (isValid == true)
            {
                Renderer.material.SetColor("_Color", Color.green);
            }
            else
            {
                Renderer.material.SetColor("_Color", Color.red);
            }
        }
    }
    private bool isValid = true;

    [SerializeField]
    private Material ghostMat;
    private MasterInput input;
    private MeshRenderer Renderer;
    private int mask;


    private void Awake()
    {
        mask = 1 << 8;
        input = new MasterInput();
        Renderer = GetComponent<MeshRenderer>();
        Renderer.material = ghostMat;
        input.Builder.MouseMove.performed += OnMouseMovePerformed;
    }


    private void OnMouseMovePerformed(InputAction.CallbackContext context)
    {

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        //Raycast against the terrain
        //256 -> 10000000 8th bit
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
        {
            Debug.DrawLine(ray.origin, hit.point);
            this.transform.position = hit.point;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.black);
        }
    }


    public void FixedUpdate()
    {
        IsValid = true;
    }

    private void OnEnable()
    {
        input.Builder.MouseMove.Enable();
    }

    private void OnDisable()
    {
        input.Builder.MouseMove.Disable();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsValid)
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            IsValid = false;
        }
    }
}
