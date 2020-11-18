using UnityEngine;
using System.Collections;
using System;


//Class describing !basic! enemy properties
public class Enemy : MonoBehaviour, ITargetable
{
    [SerializeField]
    private string enemyName = "Enemy!";
    public string EnemyName
    {
        get { return enemyName; }
    }

    public event Action<ITargetable> TargetNoLongerValid;

    public bool IsValidTarget()
    {
        return GetComponent<HealthComponent>() != null; //Check if target is killable
    }

    private void Awake()
    {
        EntityRegister.Enemies.Add(this);
    }

    private void OnDestroy()
    {
        TargetNoLongerValid?.Invoke(this); //Tell targeters that this enemy died so it's no longer valid target
        EntityRegister.Enemies.Remove(this);
    }


}
