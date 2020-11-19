using UnityEngine;
using System.Collections;


    public class ResourceGenerator : MonoBehaviour
    {
        public StrategicResource generatedResource;
        public float speed;
        public int amount;


        private ConcreteResource r;


        // Use this for initialization
        void Start()
        {
            r = PlayerResources.Instance.Resources.Find(x => x.resource == generatedResource);
            StartCoroutine(GenerateResource());
        }


        IEnumerator GenerateResource()
        {
            while (true)
            {
                r.Amount += amount;
                yield return new WaitForSeconds(speed);
            }
        }
    }
