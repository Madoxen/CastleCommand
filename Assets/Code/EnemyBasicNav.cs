using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

//Leads enemy to the nearest building
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBasicNav : MonoBehaviour
{

    NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(SearchNearest());
    }

    IEnumerator SearchNearest()
    {
        while (true)
        {
            if (navAgent.destination == this.transform.position)
            {
                FindNearestBuilding();
            }
            yield return new WaitForSeconds(2f);
        }
    }


    void FindNearestBuilding()
    {
        Vector3 pos = EntityRegister.Buildings.Aggregate((x, y) =>
        {
            if (Vector3.Distance(this.transform.position, y.transform.position) < Vector3.Distance(this.transform.position, x.transform.position))
            {
                return y;
            }
            return x;
        }).transform.position;
        navAgent.destination = pos;
    }



}
