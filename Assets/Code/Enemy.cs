using UnityEngine;
using System.Collections;
using System;


//Class describing !basic! enemy properties
//TODO: change to Unit
public class Enemy : MonoBehaviour, ITargetable
{
    [SerializeField]
    private string enemyName = "Enemy!";
    [SerializeField]
    private float points = 100;
    public string EnemyName
    {
        get { return enemyName; }
    }

    Team team;
    public Team Team
    {
        get => team;
        set
        {
            if (value == null)
            {
                TeamRegister.UnregisterTeamMember(this);
            }
            else
            {
                TeamRegister.UnregisterTeamMember(this);
                TeamRegister.RegisterTeamMember(this, value);
            }
            team = value;
        }
    }

    public event Action<ITargetable> TargetNoLongerValid;


    public bool IsValidTarget()
    {
        return GetComponent<HealthComponent>() != null; //Check if target is killable
    }



    private void Awake()
    {
        EntityRegister.Enemies.Add(this);
        Team = TeamRegister.GetTeamByName("Enemies");
    }

    private void OnDestroy()
    {
        PlayerPoints.Value += points;
        EntityRegister.Enemies.Remove(this);
        TeamRegister.UnregisterTeamMember(this);
        TargetNoLongerValid?.Invoke(this); //Tell targeters that this enemy died so it's no longer valid target
        TargetNoLongerValid = null;
        
    }


}
