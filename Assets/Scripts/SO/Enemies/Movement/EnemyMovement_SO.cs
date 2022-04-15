using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Movement", menuName = "ScriptableObjects/Movement/Enemy")]
public class EnemyMovement_SO : ScriptableObject
{
    protected GameObject unit;
    protected Rigidbody rb;
    protected float speed;
    protected Transform target;

    public void Init(GameObject _unit, Transform _target, float _speed)
    {
        unit = _unit;   
        speed = _speed;
        target = _target;
        rb = _unit.GetComponent<Rigidbody>();
    }

    public void PostInit()
    {

    }

    public void FixedRefresh()
    {

    }

    public void Refresh()
    {
        MoveToPoint();
    }

    public void MoveToPoint()
    { 
        rb.velocity = speed * (target.position - unit.transform.position).normalized;
    }
}
