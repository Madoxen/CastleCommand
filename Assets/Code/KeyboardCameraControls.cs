using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardCameraControls : MonoBehaviour
{
    public float speed = 0.5f;


    MasterInput input;

    // Start is called before the first frame update
    void Awake()
    {
        input = new MasterInput();
    }


    private void Update()
    {
        Vector2 axis = input.Camera.KeyboardMove.ReadValue<Vector2>();
        transform.position = transform.position + new Vector3(axis.x * speed, 0, axis.y * speed);
    }

    private void OnEnable()
    {
        input.Camera.KeyboardMove.Enable();
    }

    private void OnDisable()
    {
        input.Camera.KeyboardMove.Disable();
    }

}