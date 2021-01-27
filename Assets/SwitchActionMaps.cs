using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SwitchActionMaps : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private MasterInput master;

    private void Start()
    {
        master = MasterInputProvider.input;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            master.Builder.Disable();
            master.Destroyer.Disable();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            master.Builder.Enable();
            master.Destroyer.Enable();
        }
    }
}
