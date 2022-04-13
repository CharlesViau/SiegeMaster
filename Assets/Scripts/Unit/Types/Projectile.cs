using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
[RequireComponent(typeof(Rigidbody))]
 public class Projectile : MonoBehaviour,IUpdatable,IPoolable,ICreatable<Projectile.Args>
{
    [HideInInspector]
    protected float speed;
    [HideInInspector]
    protected Transform target;
    // public Vector3 direction;
    [HideInInspector]
    public DamageSO damage_SO;
    public Movement_SO movement_SO;


    private void Awake()
    {

    }
    public void Init()
    {
        damage_SO = Instantiate(damage_SO);
        movement_SO = Instantiate(movement_SO);
    }

    public void PostInit()
    {

        damage_SO.Init(gameObject);
        if (target)
        {
            movement_SO.Init(gameObject, target, speed);
        }
        else
        {
            Debug.Log("target is null");
        }
    }

    public void Refresh()
    {
        damage_SO.Refresh();
        movement_SO.Refresh();
    }

    public void FixedRefresh()
    {
    }



    private void OnCollisionEnter(Collision collision)
    {
        damage_SO.OnCollisionEnter();
        IHittable ihit = collision.gameObject.GetComponent<IHittable>();
        if (ihit != null)
            ihit.GotShot(damage_SO.damage);
        this.gameObject.SetActive(false);
    }

    public void Pool()
    {
    }

    public void Depool()
    {
    }

    public void Construct(Args constructionArgs)
    {

    }

    public class Args :ConstructionArgs
    {
        public float bulletSpeed;
        public Transform target;
       
        public Args(float _bulletSpeed, Transform _target)
        {
            bulletSpeed = _bulletSpeed;
            target = _target;
        }


    }
}
