using UnityEngine;
using System.Collections;


public class ResourceGenerator : MonoBehaviour, ITooltipDescriptor
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

    public string CreateDescription()
    {
        return "<style=Stats>Generates <sprite=\"GameIcons\" name=\"" + generatedResource.icon.name + "\"/> " + amount + " / " + speed + "s</style>";
    }
}
