using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Destroyer : MonoBehaviour
{
    Camera camera;
    RaycastHit hit;
    int buildingsLayer;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        buildingsLayer = LayerMask.GetMask("Buildings");
        Debug.Log(buildingsLayer);
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
