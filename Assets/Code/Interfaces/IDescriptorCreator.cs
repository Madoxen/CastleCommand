using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Gives human readable information about
//given component/gamesystem
public interface IDescriptorCreator
{
    Descriptor CreateDescription();
}


public struct Descriptor
{
    public int priority; //in-group priority
    public DescriptorGroup group; //descriptor grouping
    public string text;
}


public enum DescriptorGroup
{ 
    GENERAL = 0, 
    COST = 1,
    STATS = 2,
    WARNINGS = 3
}

