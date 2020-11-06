using Assets;
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
    GameObject currentBuildingPrefab;
    BuildingGhost ghost;
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    MasterInput input;


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
        ghost.enabled = true;
        input.Builder.CancelBuild.performed += CancelBuild_performed;
        input.Builder.ConfirmBuild.performed += ConfirmBuild_performed;

    }

    private void ConfirmBuild_performed(InputAction.CallbackContext obj)
    {
        if(ghost.IsValid)
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
            return;
        }

        Mesh m = buildingPrefab.GetComponent<MeshFilter>().sharedMesh;
        //Activate the building ghost
        meshFilter.mesh = m;
        meshCollider.sharedMesh = m;
        ghost.enabled = true;
    }

    //Instantiate currentBuildingPrefab at given position
    private void Build(Vector3 worldCoords)
    {
        Instantiate(CurrentBuildingPrefab, worldCoords, Quaternion.identity);
    }

}
