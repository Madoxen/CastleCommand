using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraControls : MonoBehaviour
{
    public Vector2 center = new Vector2(90f,100f);
    public float maxDistanceFromCenter = 10f;
    public float maxHeight = 8f;
    public float minHeight = 2f;

    public float speed = 2f;
    public float zoomingSpeed = 0.5f;
    public bool moveAllowed = false;

    MasterInput input;

    // Start is called before the first frame update
    void Awake()
    {
        input = new MasterInput();
        input.Camera.MoveAllowed.started += MoveAllowed_Started;
        input.Camera.MoveAllowed.canceled += MoveAllowed_Canceled;
    }


    private void Update()
    {
        Vector2 axis = Camera.main.ScreenToViewportPoint(input.Camera.Move.ReadValue<Vector2>());
        float zoom = input.Camera.Zoom.ReadValue<float>();
        Vector3 camPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);

        Debug.Log(zoom);

        if (moveAllowed) camPos = transform.position - speed * transform.position.y * (Quaternion.Euler(0f,transform.rotation.eulerAngles.y,0f) * (new Vector3(axis.x, 0, axis.y)));
        camPos += (Quaternion.Euler(-transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f) * (new Vector3(0, zoom * zoomingSpeed, 0)));


        transform.position = bound(camPos);
    }

    void MoveAllowed_Started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveAllowed = true;
    }


    void MoveAllowed_Canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveAllowed = false;
    }

    private void OnEnable()
    {
        input.Camera.Move.Enable();
        input.Camera.MoveAllowed.Enable();
        input.Camera.Zoom.Enable();
    }

    private void OnDisable()
    {
        input.Camera.Move.Disable();
        input.Camera.MoveAllowed.Disable();
        input.Camera.Zoom.Disable();
    }


    private Vector3 bound(Vector3 position)
    {
        float x = position.x;
        float y = position.y;
        float z = position.z;

        if (x > center.x+maxDistanceFromCenter) x = center.x + maxDistanceFromCenter;
        else if (x < center.x - maxDistanceFromCenter) x = center.x - maxDistanceFromCenter;

        if (y > maxHeight) y = maxHeight;
        else if (y < minHeight) y = minHeight;

        if (z > center.y + maxDistanceFromCenter) z = center.y + maxDistanceFromCenter;
        else if (z < center.y - maxDistanceFromCenter) z = center.y - maxDistanceFromCenter;

        return new Vector3(x, y, z);

    }
}
