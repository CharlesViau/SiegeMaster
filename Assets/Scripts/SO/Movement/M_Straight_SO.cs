using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Movenemt", menuName = "ScriptableObjects/Movement/Stright")]

public class M_Straight_SO : Movement_SO
{
    public override void Init(GameObject _unit, Transform _targetTransform, float speed)
    {
        base.Init(_unit, _targetTransform, speed);
        rb.velocity = (target.position - unit.transform.position).normalized * initialSpeed;
    }

}
