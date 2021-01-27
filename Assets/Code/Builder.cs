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
    [SerializeField]
    private Destroyer d;
    private List<IBuildingRule> currentBuildingRules;

    public GameObject CurrentBuildingPrefab
    {
        get { return currentBuildingPrefab; }
        set { SelectPrefab(value); }
    }

    // Start is called before the first frame update
    void Awake()
    {
        input = MasterInputProvider.input;
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

        //Combine meshes for ghost
        MeshFilter[] filters = buildingPrefab.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[filters.Length];

        int i = 0;
        while (i < filters.Length)
        {
            combine[i].mesh = filters[i].sharedMesh;
            combine[i].transform = filters[i].transform.localToWorldMatrix;
            i++;
        }

        Mesh m = new Mesh();
        m.CombineMeshes(combine);

        meshFilter.mesh = m;
        meshCollider.sharedMesh = m;
        

        ghost.enabled = true;
        renderer.enabled = true;
        ghost.moveable = true;
        d.gameObject.SetActive(false);
            
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


