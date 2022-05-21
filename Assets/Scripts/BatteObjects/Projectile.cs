using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
[RequireComponent(typeof(Rigidbody))]
 public class Projectile : MonoBehaviour,IUpdatable,IPoolable,ICreatable<Projectile.Args>
{

    [HideInInspector]public bool ownerIsPlayer;
    public ProjectileType type;
    public Movement_SO movement_SO;
    public OnCollisionSO onCollision_SO;
    public float timeToPoolIfDidntHitAnything;
   
    float timer;
    public void Init()
    {
        timer = 0;
        //only called on the 
        movement_SO = Instantiate(movement_SO);
        onCollision_SO = Instantiate(onCollision_SO);
    }

    public void PostInit()
    {


    }

    public void Refresh()
    {
        timer += Time.deltaTime;
        if (timer> timeToPoolIfDidntHitAnything )
        {
            ProjectileManager.Instance.Pool(type, this);
        }
        movement_SO.Refresh();
    }

    public void FixedRefresh()
    {
    }

    public void LateRefresh()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {

        onCollision_SO.OnEnterCollision(collision.contacts[0].point, type, this, collision,ownerIsPlayer);
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
        timer = 0;
        ownerIsPlayer = constructionArgs.isPlayer;
        transform.position = constructionArgs.spawningPosition;
        movement_SO.Init(gameObject, constructionArgs.target, constructionArgs.bulletSpeed, constructionArgs.velocityDirection);
        onCollision_SO.Init(gameObject, constructionArgs.bulletDamage, constructionArgs.isPlayer);
    }

    public class Args :ConstructionArgs
    {
        public float bulletSpeed;
        public Transform target;
        public float bulletDamage;
        public Vector3 velocityDirection;
        public ProjectileType type;
        public bool isPlayer;

        public Args(Vector3 _spawningPosition,ProjectileType _type,Transform _target, float _bulletSpeed, float _bulletDamage,Vector3 _velocityDirection,bool _isPlayer) : base(_spawningPosition)
        {
            bulletSpeed = _bulletSpeed;
            type = _type;
            target = _target;
            bulletDamage = _bulletDamage;
            velocityDirection = _velocityDirection; 
            isPlayer = _isPlayer;
        }

    }
}
