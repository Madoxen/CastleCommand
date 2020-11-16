using UnityEngine;
using System.Collections;


public interface IBuildingRule
{
    bool IsRuleValid();
    void AfterBuildEffect();
}
      