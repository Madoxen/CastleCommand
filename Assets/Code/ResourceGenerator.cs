using UnityEngine;
using System.Collections;

namespace Assets.Code
{
    public class ResourceGenerator : MonoBehaviour
    {
        public StrategicResource generatedResource;
        public float speed;
        public int amount;


        private ConcreteResource r;


        // Use this for initialization
        void Start()
        {
            StartCoroutine(GenerateResource());
            r = PlayerResources.Instance.Resources.Find(x => x.resource == generatedResource);
        }


        IEnumerator GenerateResource()
        {
            r.Amount += amount;
            yield return new WaitForSeconds(speed);
        }
    }
}