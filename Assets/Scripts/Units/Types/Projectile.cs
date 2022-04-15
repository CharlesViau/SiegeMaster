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
    public OnCollisionSO onCollision_SO;
    public void Init()
    {
        //only called on the 
        damage_SO = Instantiate(damage_SO);
        movement_SO = Instantiate(movement_SO);
        onCollision_SO = Instantiate(onCollision_SO);
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

        onCollision_SO.OnEnterCollision(collision.contacts[0].point, type,this);
     //   ObjectPool.Instance.Pool(type,this);
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
        onCollision_SO.Init(gameObject, constructionArgs.bulletDamage);
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
