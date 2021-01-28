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
    bool moveAllowed = false;
    bool rotateAllowed = false;

    public float rotationSpeed = 0.01f;
    public float expansion = 0.01f;

    MasterInput input;

    // Start is called before the first frame update
    void Awake()
    {
        input = MasterInputProvider.input;
        input.Camera.MoveAllowed.started += MoveAllowed_Started;
        input.Camera.MoveAllowed.canceled += MoveAllowed_Canceled;
        input.Camera.RotateAllowed.started += RotateAllowed_started;
        input.Camera.RotateAllowed.canceled += RotateAllowed_canceled;
    }

    private void Update()
    {
        Vector2 axis = Camera.main.ScreenToViewportPoint(input.Camera.Move.ReadValue<Vector2>());
        float zoom = input.Camera.Zoom.ReadValue<float>();

        Vector3 camPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);

        if (moveAllowed) camPos = transform.position - speed * transform.position.y * (Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f) * (new Vector3(axis.x, 0, axis.y)));

        if (rotateAllowed)
        {
            var rotation = input.Camera.Rotate.ReadValue<Vector2>();
            Camera.main.transform.RotateAround(gameObject.transform.position, Vector3.down, rotation.x * rotationSpeed);

            var newCameraRotationX = Camera.main.transform.rotation.eulerAngles.x + rotation.y * rotationSpeed;

            if (newCameraRotationX > 270 && newCameraRotationX < 352)
            {
                Camera.main.transform.rotation = Quaternion.Euler(352, Camera.main.transform.rotation.eulerAngles.y, Camera.main.transform.rotation.eulerAngles.z);
            }
            else if (newCameraRotationX > 90 && newCameraRotationX < 270)
            {
                Camera.main.transform.rotation = Quaternion.Euler(90, Camera.main.transform.rotation.eulerAngles.y, Camera.main.transform.rotation.eulerAngles.z);
            }
            else
            {
                Camera.main.transform.rotation = Quaternion.Euler(newCameraRotationX, Camera.main.transform.rotation.eulerAngles.y, Camera.main.transform.rotation.eulerAngles.z);
            }
        }

        //camPos += Quaternion.Euler(-transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f) * new Vector3(0, zoom * zoomingSpeed, 0);

        camPos += transform.forward * zoom * zoomingSpeed;


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

    private void RotateAllowed_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        rotateAllowed = true;
    }

    private void RotateAllowed_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        rotateAllowed = false;
    }

    private void OnEnable()
    {
        input.Camera.Move.Enable();
        input.Camera.MoveAllowed.Enable();
        input.Camera.Zoom.Enable();
        input.Camera.RotateAllowed.Enable();
        input.Camera.Rotate.Enable();
    }

    private void OnDisable()
    {
        input.Camera.Move.Disable();
        input.Camera.MoveAllowed.Disable();
        input.Camera.Zoom.Disable();
        input.Camera.RotateAllowed.Disable();
        input.Camera.Rotate.Disable();
    }


    private Vector3 bound(Vector3 position)
    {
        float y = position.y;
        float x = position.x;
        float z = position.z;

        if (x > center.x + maxDistanceFromCenter + y * expansion) x = center.x + maxDistanceFromCenter + y * expansion;
        else if (x < center.x - maxDistanceFromCenter - y * expansion) x = center.x - maxDistanceFromCenter - y * expansion;

        //if (y > maxHeight) y = maxHeight;
        if (y < minHeight) y = minHeight;

        if (z > center.y + maxDistanceFromCenter + y * expansion) z = center.y + maxDistanceFromCenter + y * expansion;
        else if (z < center.y - maxDistanceFromCenter - y * expansion) z = center.y - maxDistanceFromCenter - y * expansion;

        return new Vector3(x, y, z);

    }
}
