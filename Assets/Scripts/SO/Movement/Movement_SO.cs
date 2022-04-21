using UnityEngine;
using General;
using System;
using System.Reflection;

public class Movement_SO : ScriptableObject
{
    protected GameObject gameobject;
    
    public Transform target;
    public Rigidbody rb;
    public Vector3 projectileInitialDIrection;
    public float initialSpeed;
    public Type type;
    public virtual void FixedRefresh()
    {
    }


    public virtual void Init(GameObject _gameObject, Transform _targetTransform,float speed,Vector3 _projectileInitialDIrection)
    {

        gameobject = _gameObject;
        target = _targetTransform;
        rb = gameobject.GetComponent<Rigidbody>();
        initialSpeed = speed;
        projectileInitialDIrection = _projectileInitialDIrection;
    }

    public virtual void Refresh()
    {
    }
}
