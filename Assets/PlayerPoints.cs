using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPoints
{
    private static float _value;
    public static float Value {
        get => _value;
        set { _value = value; ValueChanged?.Invoke(_value); }
    }

    public delegate void ValueChangedEventHandler(float value);
    public static event ValueChangedEventHandler ValueChanged;

    public static void Reset()
    {
        Value = 0;
    }
}
