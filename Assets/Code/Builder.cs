using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Responsible for positioning and instantiating buildings
//Also shows building ghost if a building is chosen
[RequireComponent(typeof(BuildingGhost))]
public class Builder : MonoBehaviour //IMPROV: Make it a singleton? todo: talk about it with the team if this makes sense 
{
    [SerializeField]
    private GameObject currentBuildingPrefab;
    private BuildingGhost ghost;
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;
    private new MeshRenderer renderer;
    private MasterInput input;
    

    public GameObject CurrentBuildingPrefab
    {
        get { return currentBuildingPrefab; }
        set { SelectPrefab(value); }
    }


    // Start is called before the first frame update
    void Awake()
    {
        input = new MasterInput();
        ghost = GetComponent<BuildingGhost>();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        renderer = GetComponent<MeshRenderer>();
        

        input.Builder.CancelBuild.performed += CancelBuild_performed;
        input.Builder.ConfirmBuild.performed += ConfirmBuild_performed;

        SelectPrefab(null);
    }

    private void ConfirmBuild_performed(InputAction.CallbackContext obj)
    {
        if(CurrentBuildingPrefab != null && ghost.IsValid)
            Build(transform.position);
    }

    private void CancelBuild_performed(InputAction.CallbackContext obj)
    {
        CurrentBuildingPrefab = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        input.Builder.Enable();
    }

    private void OnDisable()
    {
        input.Builder.Disable();
    }


    //TODO: research property serialization and possible event call on assignment
    private void SelectPrefab(GameObject buildingPrefab)
    {
        currentBuildingPrefab = buildingPrefab;
        if (buildingPrefab == null)
        {
            ghost.enabled = false;
            renderer.enabled = false;
            return;
        }

        Mesh m = buildingPrefab.GetComponent<MeshFilter>().sharedMesh;
        //Activate the building ghost
        meshFilter.mesh = m;
        meshCollider.sharedMesh = m;
        ghost.enabled = true;
        renderer.enabled = true;
    }

    //Instantiate currentBuildingPrefab at given position
    private void Build(Vector3 worldCoords)
    {
        Instantiate(CurrentBuildingPrefab, worldCoords, Quaternion.identity);
    }

}
