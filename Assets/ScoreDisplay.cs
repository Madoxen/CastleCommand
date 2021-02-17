using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private Text Value;

    private void Awake()
    {
        PlayerPoints.Reset();
        PlayerPoints.ValueChanged += OnPlayerPointsChanged;
    }

    private void OnPlayerPointsChanged(float value)
    {
        Value.text = value.ToString();
    }
}
