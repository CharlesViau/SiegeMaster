using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Movenemt", menuName = "ScriptableObjects/Movement/Catapult")]

public class M_Catapult_SO : Movement_SO
{

    public override void Init(GameObject _unit, ProjectileType _type, Transform _targetTransform, float _speed, Vector3 _projectileInitialDIrection)
    {
        base.Init(_unit,_type ,_targetTransform, _speed, _projectileInitialDIrection);
        rb.velocity = _projectileInitialDIrection;
    }

}
