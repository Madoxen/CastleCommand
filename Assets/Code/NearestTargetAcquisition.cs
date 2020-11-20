using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class NearestTargetAcquisition : MonoBehaviour, ITargetAcquisition
{
    public event Action<ITargetable> TargetAcquired;
    public int targetTeamID = 0;

    private ITargetable currentAttackTarget = null;
    public ITargetable CurrentAttackTarget
    {
        get { return currentAttackTarget; }
        private set { currentAttackTarget = value; TargetAcquired?.Invoke(CurrentAttackTarget); }
    }

    public float searchRefresh = 1f;
    Coroutine searchCoroutine;


    void OnEnable()
    {
        searchCoroutine = StartCoroutine(SearchNearest());
    }

    void OnDisable()
    {
        if(searchCoroutine != null)
            StopCoroutine(searchCoroutine);
    }


    private void OnCurrentTargetNoLongerValid(ITargetable sender)
    {
        sender.TargetNoLongerValid -= this.OnCurrentTargetNoLongerValid; 
        CurrentAttackTarget = null;
        searchCoroutine = StartCoroutine(SearchNearest());
    }

    IEnumerator SearchNearest()
    {
        while (true)
        {
            if (CurrentAttackTarget == null)
            {
                FindNearest();
            }
            yield return new WaitForSeconds(searchRefresh);
        }
    }


    void FindNearest()
    {
        Team targetTeam = TeamRegister.GetTeamByID(targetTeamID);
        if (targetTeam.TeamMemebers.Count == 0)
        {
            return;
        }
        
        ITeamable teamable = targetTeam.TeamMemebers.Aggregate((x, y) =>
        {
            if (!(x is MonoBehaviour a))
                return y;
            if (!(y is MonoBehaviour b))
                return x;

            if (Vector3.Distance(this.transform.position, a.transform.position) < Vector3.Distance(this.transform.position, b.transform.position))
            {
                return y;
            }
            return x;
        });

        if (!(teamable is ITargetable target))
            return;

        CurrentAttackTarget = target;
        CurrentAttackTarget.TargetNoLongerValid += OnCurrentTargetNoLongerValid;

        if (searchCoroutine != null)
            StopCoroutine(searchCoroutine); 
    }

    private void OnDestroy()
    {
        TargetAcquired = null;
        CurrentAttackTarget.TargetNoLongerValid -= OnCurrentTargetNoLongerValid;
    }
}

