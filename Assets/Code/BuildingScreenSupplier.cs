using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingScreenSupplier : MonoBehaviour
{
    [SerializeField]
    private BuildingListSC sc;
    public BuildingListSC SC //list to take our prefabs from and generate appropiate buttons for them
    {
        get { return sc; }
        set { sc = value; GenerateScreen(); }
    }


    [SerializeField]
    private GameObject buttonPrefab; // button prefab that will be used in screen generation

    [SerializeField]
    private Builder builder; //reference to builder object

    private void Awake()
    {
        GenerateScreen();
    }


    //Fills UI screen with buttons
    void GenerateScreen()
    {
        if (SC == null)
            throw new Exception("List SC is null, cannot generate screen");

        if (buttonPrefab == null)
            throw new Exception("Button Prefab is null, cannot generate screen");

        //Clear screen
        foreach (Transform child in transform)
        {
            Destroy(child);
        }

        //Add new buttons
        foreach (GameObject prefab in sc.prefabList)
        {
            GameObject button = Instantiate(buttonPrefab, this.transform);
            button.name = "button_build" + prefab.name;
            //TODO: customize buttons based on prefab


            Button b = button.GetComponent<Button>();
            b.onClick.AddListener(() =>
            {
                builder.CurrentBuildingPrefab = prefab;
            });
        }
    }
}
