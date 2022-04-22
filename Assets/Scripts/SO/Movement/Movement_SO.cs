using UnityEngine;
using Units.Types;
using General;

public class Movement_SO : ScriptableObject
{
    public ProjectileType type;
    [HideInInspector]protected GameObject gameobject;
    [HideInInspector]public IPoolable ipoolable;
    [HideInInspector]public Transform target;
    [HideInInspector]public Rigidbody rb;
    [HideInInspector]public Vector3 projectileInitialDIrection;
    [HideInInspector]public float initialSpeed;
    
    public virtual void FixedRefresh()
    {
    }


    public virtual void Init(GameObject _gameObject, ProjectileType _type, Transform _target,float speed,Vector3 _projectileInitialDIrection)
    {
        gameobject = _gameObject;
        ipoolable = gameobject.GetComponent<IPoolable>();
        type = _type;
        target = _target;
        rb = gameobject.GetComponent<Rigidbody>();
        initialSpeed = speed;
        projectileInitialDIrection = _projectileInitialDIrection;
    }

    public virtual void Refresh()
    {
    }
}
