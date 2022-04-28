using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using System;
using System.Reflection;
[CreateAssetMenu(fileName = "OnCollision", menuName = "ScriptableObjects/OnCollision/HitObject")]

public class OnCollition_CollideWithObject_SO : OnCollisionSO
{
    public override void OnEnterCollision(Vector3 position,ValueType type,IPoolable type2, Collision collisionObject) 
    {

            ObjectPool.Instance.Pool(type, type2);
            ParticleSystemManager.Instance.Create(onCollisionParticleType, new ParticleSystemScript.Args(position));

            //deal damage to object that had Ihittable interface and deal damage
            var p = collisionObject.collider.gameObject.GetComponent(typeof(IHittable));
            if (p != null)
            {
                ((IHittable)p).GotShot(damage);
            }
        
    }
}
