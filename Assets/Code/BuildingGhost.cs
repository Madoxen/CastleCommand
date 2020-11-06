using UnityEngine;
using System.Collections;

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

        MeshRenderer Renderer;

        private void Awake()
        {
            Renderer = GetComponent<MeshRenderer>();
            Renderer.material = ghostMat;
        }


        void OnTriggerEnter(Collider other)
        {
            if (gameObject.layer == LayerMask.NameToLayer("Buildings"))
                CollisionCount++;
        }

        void OnTriggerExit(Collider other)
        {
            if (gameObject.layer == LayerMask.NameToLayer("Buildings"))
                CollisionCount--;
        }
    }
}