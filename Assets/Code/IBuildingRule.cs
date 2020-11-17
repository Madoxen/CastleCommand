using UnityEngine;
using System.Collections;

public interface IBuildingRule
{
    bool IsRuleValid();
    void AfterBuildEffect();
    void Init(Builder b);

    void Dispose();
}
