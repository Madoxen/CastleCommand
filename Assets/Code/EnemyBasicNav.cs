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
    Building target;

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
            if (target == null)
            {
                FindNearestBuilding();
            }
            yield return new WaitForSeconds(1f);
        }
    }


    void FindNearestBuilding()
    {
        target = EntityRegister.Buildings.Aggregate((x, y) =>
        {
            if (Vector3.Distance(this.transform.position, y.transform.position) < Vector3.Distance(this.transform.position, x.transform.position))
            {
                return y;
            }
            return x;
        });
        navAgent.destination = target.transform.position;
    }



}
