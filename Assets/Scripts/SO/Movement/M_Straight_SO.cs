using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Movenemt", menuName = "ScriptableObjects/Movement/Stright")]

public class M_Straight_SO : Movement_SO
{
    public override void Init(GameObject _unit, Transform _targetTransform, float speed,Vector3 _projectileInitialDIrection)
    {
        base.Init(_unit, _targetTransform, speed, _projectileInitialDIrection);
        _unit.transform.forward = (_targetTransform.position - _unit.transform.position).normalized;
        rb.velocity = (_targetTransform.position - _unit.transform.position).normalized * initialSpeed;
    }

}
