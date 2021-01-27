using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance = null;
    public static Tooltip Instance
    {
        get { return instance; }
    }

    private TextMeshProUGUI t;
    private static MasterInput input;
    private RectTransform rectTransform;
    private static Vector2 padding = new Vector2(30, 30);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Singleton instance already registered; this instance will be destroyed");
            Destroy(this);
            return;
        }

        t = GetComponentInChildren<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        input = MasterInputProvider.input;
        HideTooltip();
    }


    private void OnEnable()
    {
        input.UI.Point.Enable();
    }

    private void OnDisable()
    {
        input.UI.Point.Disable();
    }

    private void Update()
    {
        transform.position = input.UI.Point.ReadValue<Vector2>();
    }

    public static void ShowTooltip(string text)
    {
        if (string.IsNullOrEmpty(text))
            return;
        Instance.gameObject.SetActive(true);
        Instance.transform.position = input.UI.Point.ReadValue<Vector2>();
        Instance.t.text = text;
        //TODO: resize
        Vector2 textSize = Instance.t.GetPreferredValues();
        Instance.rectTransform.sizeDelta = textSize;
    }

    public static void HideTooltip()
    {
        Instance.gameObject.SetActive(false);
    }


}
