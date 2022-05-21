using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using System;
using System.Reflection;
using Units.Interfaces;

[CreateAssetMenu(fileName = "OnCollision", menuName = "ScriptableObjects/OnCollision/HitObject")]

public class OnCollition_CollideWithObject_SO : OnCollisionSO
{
    public override void OnEnterCollision(Vector3 position, ValueType type, IPoolable type2, Collision collisionObject, bool isPlayer)
    {


        ParticleSystemManager.Instance.Create(onCollisionParticleType, new ParticleSystemScript.Args(position));

        //deal damage to object that had Ihittable interface and deal damage
        var p = collisionObject.collider.gameObject.GetComponent(typeof(IHittable));
        if (p != null)
        {

            if (isPlayer && p.gameObject.tag=="Target")
            {
                ((Units.Types.Enemy)p).GotShot(damage);
            }
            if (!isPlayer && p.gameObject.tag == "Player")
            {
                ((Units.Types.PlayerUnit)p).GotShot(damage);
            }
          
        }

    }
}
