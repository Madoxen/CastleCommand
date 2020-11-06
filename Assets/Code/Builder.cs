using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Responsible for positioning and instantiating buildings
//Also shows building ghost if a building is chosen
public class Builder : MonoBehaviour
{
    [SerializeField]
    GameObject currentBuildingPrefab;
    [SerializeField]
    GameObject buildingGhostObject;


    public GameObject CurrentBuildingPrefab
    {
        get { return currentBuildingPrefab; }
        set { selectPrefab(value); }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

     



    }

    
    //TODO: research property serialization and possible event call on assignment
    public void selectPrefab(GameObject buildingPrefab)
    {
        currentBuildingPrefab = buildingPrefab;
        Mesh m = buildingPrefab.GetComponent<MeshFilter>().mesh;


        //Activate the building ghost
        buildingGhostObject.GetComponent<MeshFilter>().mesh = m;
        buildingGhostObject.GetComponent<MeshCollider>().sharedMesh = m;
        buildingGhostObject.SetActive(true);
    }
}
