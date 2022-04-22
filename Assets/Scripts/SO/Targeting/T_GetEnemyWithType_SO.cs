using General;
using Managers;
using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Targeting", menuName = "ScriptableObjects/Targeting/GetEnemyWithType")]

public class T_GetEnemyWithType_SO : Targeting_SO
{
    public EnemyType enemyType;
    public override Transform GetTheTarget()
    {
        return null;
    }

}
