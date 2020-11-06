using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace Assets
{
    public class BuildingGhost : MonoBehaviour
    {
        bool IsValid
        {
            get { return isValid; }
            set
            {
                isValid = value;
                if (isValid == true)
                {
                    Renderer.material.SetColor("_Color", Color.green);
                }
                else
                {
                    Renderer.material.SetColor("_Color", Color.red);
                }
            }
        }
        bool isValid = true;



        int collisionCount = 0;
        int CollisionCount
        {
            get { return collisionCount; }
            set
            {
                collisionCount = value;
                IsValid = (collisionCount == 0);
            }
        }

        [SerializeField]
        Material ghostMat;
        MasterInput input;
        MeshRenderer Renderer;
        int mask;

        private void Awake()
        {
            mask = 1 << 8;
            input = new MasterInput();
            Renderer = GetComponent<MeshRenderer>();
            Renderer.material = ghostMat;
            input.Player.Mouse.performed += GhostMoved_performed;
        }

        private void GhostMoved_performed(InputAction.CallbackContext context)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            //Raycast against the terrain
            //256 -> 10000000 8th bit
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
            {
                Debug.DrawLine(ray.origin, hit.point);
                this.transform.position = hit.point;
            }
            else
            {
                Debug.DrawRay(ray.origin,ray.direction,Color.black);
            }

        }


        private void OnEnable()
        {
            input.Player.Mouse.Enable();
        }

        private void OnDisable()
        {
            input.Player.Mouse.Disable();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
                CollisionCount++;
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
                CollisionCount--;
        }
    }
}