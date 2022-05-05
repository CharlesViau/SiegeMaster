using System.Collections;
using System.Collections.Generic;
using SO.TowerSo.Targeting;
using UnityEngine;

[CreateAssetMenu(fileName = "Target Objective", menuName = "ScriptableObjects/Targeting/T_TargetObject_SO")]
public class T_TargetObjective_SO : TargetingSo
{    
    // this class needs to implement

    GameObject unit;
    float range;

    public override void Init(GameObject owner, float maxRange)
    {
        base.Init(owner, maxRange);
        
    }

    public override Transform GetTheTarget()
    {
        return Owner.transform;
    }
}
