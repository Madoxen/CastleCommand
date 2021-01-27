using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

//Responsible for positioning and instantiating buildings
//Also shows building ghost if a building is chosen
[RequireComponent(typeof(BuildingGhost))]
public class Builder : MonoBehaviour //IMPROV: Make it a singleton? todo: talk about it with the team if this makes sense 
{
    [SerializeField]
    private GameObject currentBuildingPrefab;
    public BuildingGhost ghost { get; private set; }
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;
    private new MeshRenderer renderer;
    private MasterInput input;
    private AudioSource AS;
    private List<IBuildingRule> currentBuildingRules;

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
        AS = GetComponent<AudioSource>();


        input.Builder.CancelBuild.performed += CancelBuild_performed;
        input.Builder.ConfirmBuild.performed += ConfirmBuild_performed;
        input.UI.Disable();
        SelectPrefab(null);
    }

    private void ConfirmBuild_performed(InputAction.CallbackContext obj)
    {
        if (CurrentBuildingPrefab != null && ghost.IsValid)
        {
            GameObject newBuilding = Build(transform.position);
            currentBuildingRules?.ForEach(x => x.AfterBuildEffect(newBuilding));
            AS.Play();
        }
    }

    private void CancelBuild_performed(InputAction.CallbackContext obj)
    {
        CurrentBuildingPrefab = null;
    }


    private void FixedUpdate()
    {
        
        if (currentBuildingRules != null)
        {
            ghost.IsValid = currentBuildingRules.All(x => x.IsRuleValid());
        }
        else
            ghost.IsValid = true;

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
        currentBuildingRules?.ForEach(x => x.Dispose());
        currentBuildingPrefab = buildingPrefab;
        if (buildingPrefab == null)
        {
            ghost.enabled = false;
            renderer.enabled = false;
            currentBuildingRules = null;
            return;
        }

        Mesh m = buildingPrefab.GetComponent<MeshFilter>().sharedMesh;
        //Activate the building ghost
        meshFilter.mesh = m;
        meshCollider.sharedMesh = m;
        ghost.enabled = true;
        renderer.enabled = true;
        ghost.moveable = true;

        currentBuildingRules = buildingPrefab.GetComponents<IBuildingRule>().ToList();
        currentBuildingRules.ForEach(x => x.Init(this));
    }

    //Instantiate currentBuildingPrefab at given position
    private GameObject Build(Vector3 worldCoords)
    {
        GameObject building = Instantiate(CurrentBuildingPrefab, worldCoords, Quaternion.identity);
        building.GetComponents<IBuildingRule>().ToList().ForEach(x => Destroy((MonoBehaviour)x)); //we dont want building rules on already built buildings
        return building;
    }

}
