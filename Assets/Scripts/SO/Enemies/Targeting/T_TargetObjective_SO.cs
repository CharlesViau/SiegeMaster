using General;
using Managers;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Target Objective", menuName = "ScriptableObjects/Targeting/Target Nexus")]
public class T_TargetObjective_SO : Targeting_SO
{    
    Transform nexus;

    public override void Init(GameObject _unit, float _range)
    {
        base.Init(_unit, _range);
        nexus = NexusManager.Instance.GetTransform;        
    }

    public override Transform GetTheTarget()
    {
        return nexus;
    }


    /*void DetectPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < 0.1f)
        {
            Move(player.position);
        }
        else
        {
            Move(objective.transform.position);
        }
    }*/
}
