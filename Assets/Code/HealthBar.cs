using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    HealthComponent h;
    RectTransform greenBar;
    float barWidth;
    Canvas c;

    private void Awake()
    {
        h = GetComponentInParent<HealthComponent>();
        h.HealthChangedEvent += OnHealthChanged;
        h.MaxHealthChangedEvent += OnHealthChanged;
        greenBar = transform.Find("Bar").GetComponent<RectTransform>();
        barWidth = greenBar.sizeDelta.x;
        c = GetComponent<Canvas>();
        c.enabled = (float)h.CurrentHealth / (float)h.MaxHealth >= 1f ? false : true;
    }

    private void OnHealthChanged(object sender, int currentHealth)
    {
        float percent = (float)h.CurrentHealth / (float)h.MaxHealth;
        c.enabled = percent >= 1f ? false : true;
        greenBar.sizeDelta = new Vector2(barWidth * percent, greenBar.sizeDelta.y);
    }

    private void OnDestroy()
    {
        h.HealthChangedEvent -= OnHealthChanged;
        h.MaxHealthChangedEvent -= OnHealthChanged;
    }
}
