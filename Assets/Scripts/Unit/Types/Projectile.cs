using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
[RequireComponent(typeof(Rigidbody))]
 public class Projectile : MonoBehaviour,IUpdatable,IPoolable,ICreatable<Projectile.Args>
{
    public ProjectileType type;
    public DamageSO damage_SO;
    public Movement_SO movement_SO;

    public void Init()
    {
        damage_SO = Instantiate(damage_SO);
        movement_SO = Instantiate(movement_SO);
    }

    public void PostInit()
    {


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
        ObjectPool.Instance.Pool(type,this);
    }

    public void Pool()
    {
       this.gameObject.SetActive(false);    
    }

    public void Depool()
    {
        this.gameObject.SetActive(true);
        movement_SO.rb.velocity = Vector3.zero;
        movement_SO.rb.angularVelocity = Vector3.zero;

    }

    public void Construct(Args constructionArgs)
    {
        
        transform.position = constructionArgs.spawningPosition;
        damage_SO.Init(gameObject, constructionArgs.bulletDamage);
        movement_SO.Init(gameObject, constructionArgs.target, constructionArgs.bulletSpeed);
    }

    public class Args :ConstructionArgs
    {
        public float bulletSpeed;
        public Transform target;
        public float bulletDamage;

        public Args(Vector3 _spawningPosition,Transform _target, float _bulletSpeed, float _bulletDamage) : base(_spawningPosition)
        {
            bulletSpeed = _bulletSpeed;
            target = _target;
            bulletDamage = _bulletDamage;
        }

    }
}
