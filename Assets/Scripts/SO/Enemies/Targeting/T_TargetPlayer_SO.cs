using General;
using Managers;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Target Player", menuName = "ScriptableObjects/Targeting/T_TargetPlayer_SO")]
public class T_TargetPlayer_SO : Targeting_SO
{
    Transform nexus;
    Transform player;
    bool isAttacking;

    public override void Init(GameObject _unit, float _range)
    {
        base.Init(_unit, _range);
        nexus = NexusManager.Instance.GetTransform;
        player = PlayerUnitManager.Instance.GetTransform;
    }

    public override Transform GetTheTarget()
    {
        if (DetectPlayer())
            return player;
        else
            return nexus;
    }

    bool DetectPlayer()
    {
        if (Vector3.Distance(gameObject.transform.position, player.position) < range)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
        return isAttacking;
    }
}
