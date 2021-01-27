using UnityEngine;
using System.Collections;


//Represents builiding rules that can be evaluated for build validation
public interface IBuildingRule
{
    bool IsRuleValid(); //Returns if rule is valid at the current frame
    void AfterBuildEffect(GameObject newBuilding); //Something that will happen after the building is built
    void Init(Builder b); //DI for Components
    void Dispose(); //Unsubscribe from events 
}
