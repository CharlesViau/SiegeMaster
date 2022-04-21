using UnityEngine;
using UnityEngine;
using Units.Types;
public class Movement_SO : ScriptableObject
{
    protected GameObject gameobject;
    
    public Transform target;
    public Rigidbody rb;
    public Vector3 projectileInitialDIrection;
    public float initialSpeed;
    public ProjectileType type;
    public Enemy enemy;
    public virtual void FixedRefresh()
    {
    }


    public virtual void Init(GameObject _gameObject, ProjectileType _type, Transform _targetTransform,float speed,Vector3 _projectileInitialDIrection)
    {
        gameobject = _gameObject;
        type = _type;
        target = _targetTransform;
        rb = gameobject.GetComponent<Rigidbody>();
        initialSpeed = speed;
        projectileInitialDIrection = _projectileInitialDIrection;
    }

    public virtual void Refresh()
    {
    }
}
