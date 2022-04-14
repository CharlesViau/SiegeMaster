using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_SO : ScriptableObject
{
    protected GameObject gameobject;
    public Transform target;
    public Rigidbody rb;
    public float initialSpeed;
    public virtual void FixedRefresh()
    {
    }


    public virtual void Init(GameObject _gameObject,Transform _targetTransform,float speed)
    {
        gameobject = _gameObject;
        target = _targetTransform;
        rb = gameobject.GetComponent<Rigidbody>();
        initialSpeed = speed;
    }

    public virtual void Refresh()
    {
    }
}