using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float speed = 1;


    MasterInput input;

    // Start is called before the first frame update
    void Awake()
    {
        input = new MasterInput();
    }


    private void Update()
    {
        Vector2 axis = input.Camera.Move.ReadValue<Vector2>();
        transform.position = transform.position + new Vector3(axis.x * speed, 0, axis.y * speed);
    }




    private void OnEnable()
    {
        input.Camera.Move.Enable();
    }

    private void OnDisable()
    {
        input.Camera.Move.Disable();
    }

}
