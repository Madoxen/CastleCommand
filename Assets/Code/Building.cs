using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Class representing building component
//Used for registering buildings in building UI
public class Building : MonoBehaviour
{


    private void Awake()
    {
        EntityRegister.Buildings.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        EntityRegister.Buildings.Remove(this);
    }
}
