using System.Collections.Generic;

public class Team
{
    private static int TeamAmount = 0;

    private int teamID;
    public int TeamID
    {
        get { return teamID; }
    }

    public List<ITeamable> TeamMemebers { get; } = new List<ITeamable>();

    public string name;

    public Team(string name)
    {
        teamID = TeamAmount;
        this.name = name;
        TeamAmount++;
    }
}