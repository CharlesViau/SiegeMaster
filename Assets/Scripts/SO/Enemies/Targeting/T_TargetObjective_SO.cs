using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Target Objective", menuName = "ScriptableObjects/Targeting/T_TargetObject_SO")]
public class T_TargetObjective_SO : Targeting_SO
{    
    // this class needs to implement

    GameObject unit;
    float range;

    public override void Init(GameObject _unit, float _range)
    {
        base.Init(_unit, _range);
        
    }

    public override Transform GetTheTarget()
    {
        return gameObject.transform;
    }
}
