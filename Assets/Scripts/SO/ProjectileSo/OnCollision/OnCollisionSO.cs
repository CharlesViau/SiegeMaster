using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using General;
public class OnCollisionSO : ScriptableObject
{
    protected GameObject gameObject;
    protected float damage;
    
    public ParticleType onCollisionParticleType;
    public virtual void Init(GameObject gameobject, float _damage)
    {
        gameObject = gameobject;
        damage = _damage;
    }

    public virtual void OnEnterCollision(Vector3 position, ValueType type, IPoolable type2,Collision collisionObject)
    {
    }

}
