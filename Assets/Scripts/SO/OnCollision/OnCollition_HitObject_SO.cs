using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using System;
[CreateAssetMenu(fileName = "OnCollision", menuName = "ScriptableObjects/OnCollision/HitObject")]

public class OnCollition_HitObject_SO : OnCollisionSO
{
    public override void OnEnterCollision(Vector3 position,ValueType type,IPoolable type2) 
    {
        ObjectPool.Instance.Pool(type, type2);
        ParticleSystemManager.Instance.Create(onCollisionParticleType, new ParticleSystemScript.Args(position));
    }
}
