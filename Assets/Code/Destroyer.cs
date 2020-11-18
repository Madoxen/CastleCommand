using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        buildingsLayer = LayerMask.GetMask("Buildings");
    }
    
    void OnEnable()
    {
        builder.CurrentBuildingPrefab = null; //hope that's the right way to turn the builder off
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());


        if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingsLayer))
        {
            //TODO: show that the building is selected somehow like paint it red or sth

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
