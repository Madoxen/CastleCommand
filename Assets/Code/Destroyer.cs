using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;


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
    Material destroyShader;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        buildingsLayer = LayerMask.GetMask("Buildings");
        destroyShader = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Purple.2");
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
            var hitObject = hit.collider.gameObject;

            if (hitObject != lastHit) {

                if (lastHit != null)
                    lastHit.GetComponent<Renderer>().material = lastMaterial;

                lastHit = hitObject;

                lastMaterial = hitObject.GetComponent<Renderer>().material;

                hitObject.GetComponent<Renderer>().material = destroyShader;
            }


            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Destroy(hit.collider.gameObject);
            }
        }
        else if (lastHit != null)
        {
            lastHit.GetComponent<Renderer>().material = lastMaterial;
            lastHit = null;
            lastMaterial = null;
        }
    }
}
