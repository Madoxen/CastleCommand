using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EntityRegister
{
    private static List<Building> buildings = new List<Building>();
    public static List<Building> Buildings
    {
        get { return buildings; }
    }

    private static List<Enemy> enemies = new List<Enemy>();
    public static List<Enemy> Enemies
    {
        get { return enemies; }
    }

}
