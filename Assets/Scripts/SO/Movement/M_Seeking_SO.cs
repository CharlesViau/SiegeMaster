using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Types;
using System.Reflection;

using General;
[CreateAssetMenu(fileName = "Movenemt", menuName = "ScriptableObjects/Movement/Seeking")]

public class M_Seeking_SO : Movement_SO
{

    private Enemy enemy;
    public override void Refresh()
    {

        if (target == null || enemy.alive == false)
        {
            ObjectPool.Instance.Pool(type, ipoolable);
            return;
        }
        rb.velocity = (target.position - gameobject.transform.position).normalized * initialSpeed;
        gameobject.transform.forward = rb.velocity.normalized;


    }
    public override void Init(GameObject _gameObject, ProjectileType _type, Transform _target, float speed, Vector3 _projectileInitialDIrection)
    {
        base.Init(_gameObject, _type, _target, speed, _projectileInitialDIrection);
        enemy = _target.gameObject.GetComponent<Enemy>();
    }

}
