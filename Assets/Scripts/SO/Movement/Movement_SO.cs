using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_SO : ScriptableObject
{
    protected GameObject unit;
    protected Transform target;
    protected Rigidbody rb;
    protected float initialSpeed;
    public virtual void FixedRefresh()
    {
    }


    public virtual void Init(GameObject _unit,Transform _targetTransform,float speed)
    {
        unit = _unit;
        target = _targetTransform;
        rb = unit.GetComponent<Rigidbody>();
        initialSpeed = speed;
    }

    public virtual void Refresh()
    {
    }
}
