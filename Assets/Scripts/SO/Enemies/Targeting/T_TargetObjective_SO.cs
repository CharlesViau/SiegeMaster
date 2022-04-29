using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Target Objective", menuName = "ScriptableObjects/Targeting/T_TargetObject_SO")]
public class T_TargetObjective_SO : Targeting_SO
{    
    public LayerMask objective;

    public override void Init(GameObject _unit, float _range)
    {
        base.Init(_unit, _range);
        if (objective != _unit.layer)
            Debug.LogError("The object layer should be" + objective.ToString());
    }

    public override Transform GetTheTarget()
    {
        return gameObject.transform;
    }
}
