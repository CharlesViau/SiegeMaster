using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
[CreateAssetMenu(fileName = "OnCollision", menuName = "ScriptableObjects/OnCollision/HitObject")]

public class OnCollition_HitObject_SO : OnCollisionSO
{
    public override void OnEnterCollision(Vector3 position)
    {
        ObjectPool.Instance.Pool(gameObject.GetComponent<Projectile>().type, gameObject);
        ParticleSystemManager.Instance.Create(onCollisionParticleType, new ParticleSystemScript.Args(position));
    }
}
