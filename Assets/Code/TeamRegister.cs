using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class TeamRegister
{
    public static List<Team> Teams { get; } = new List<Team>()
    { 
           new Team("Player"),
           new Team("Enemies")
    };
        
    public static void RegisterTeamMember(ITeamable member, Team t)
    {
        t.TeamMemebers.Add(member);
    }

    public static void UnregisterTeamMember(ITeamable member)
    {
        if (member.Team == null)
            return;

        member.Team.TeamMemebers.Remove(member);
    }

    public static Team GetTeamByID(int id)
    {
        return Teams.FirstOrDefault(x => x.TeamID == id);
    }

    public static Team GetTeamByName(string name)
    {
        return Teams.FirstOrDefault(x => x.name == name);
    }
}
