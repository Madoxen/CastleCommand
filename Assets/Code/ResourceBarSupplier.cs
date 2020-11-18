using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//Builds resource bar
public class ResourceBarSupplier : MonoBehaviour
{

    [SerializeField]
    private GameObject itemPrefab; // button prefab that will be used in screen generation

    private void Awake()
    {
        GenerateBar();
    }


    private void GenerateBar()
    {
        if (itemPrefab == null)
            throw new Exception("Item Prefab is null, cannot generate screen");

        //Clear screen
        foreach (Transform child in transform)
        {
            Destroy(child);
        }

        //Add new Items
        foreach (ConcreteResource resource in PlayerResources.Instance.Resources)
        {
            GameObject barItem = Instantiate(itemPrefab, this.transform);
            barItem.name = "barItem_" + resource.resource.Name;
            barItem.GetComponentsInChildren<Image>()[1].sprite = resource.resource.icon; //GetComponentinChildren gets component in parent as well, so we need to skip one
            barItem.GetComponentInChildren<Text>().text = resource.Amount + "/" + resource.MaxAmount;


            //Update event
            resource.AmountChanged += (_) =>
            {
                barItem.GetComponentInChildren<Text>().text = resource.Amount + "/" + resource.MaxAmount;
            };

            resource.MaxAmountChanged += (_) =>
            {
                barItem.GetComponentInChildren<Text>().text = resource.Amount + "/" + resource.MaxAmount;
            };


            string TooltipText = "<style=\"H1\">" + resource.resource.Name + "</style>\n<style=\"Quote\">" + resource.resource.description+"</style>";

            EventTrigger trigger = barItem.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => { Tooltip.ShowTooltip(TooltipText); });
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener((data) => { Tooltip.HideTooltip(); });
            trigger.triggers.Add(entry);
        }
    }





}
