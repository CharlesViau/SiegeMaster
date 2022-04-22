using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Movenemt", menuName = "ScriptableObjects/Movement/Rolling")]

public class M_Rolling_SO : Movement_SO
{
    public override void Init(GameObject _unit, ProjectileType _type, Transform _targetTransform, float _speed, Vector3 _projectileInitialDIrection)
    {
        _unit.transform.forward = (_targetTransform.position - _unit.transform.position).normalized;
        base.Init(_unit,  _type,_targetTransform, _speed, _projectileInitialDIrection);
        rb.velocity = (_targetTransform.position - _unit.transform.position).normalized * initialSpeed;
    }   

}
