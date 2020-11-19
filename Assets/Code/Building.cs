using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Class representing building component
//Used for registering buildings in building UI
public class Building : MonoBehaviour, ITargetable
{
    public string description = "Blah Blash";

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

    private void Awake()
    {
        EntityRegister.Buildings.Add(this);
        Team = TeamRegister.GetTeamByName("Player");
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
        TargetNoLongerValid?.Invoke(this);
        EntityRegister.Buildings.Remove(this);
        TeamRegister.UnregisterTeamMember(this);
        TargetNoLongerValid = null;
    }

    public bool IsValidTarget()
    {
        return true;
    }
}
