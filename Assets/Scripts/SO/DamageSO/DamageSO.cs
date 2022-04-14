using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSO : ScriptableObject
{
    protected GameObject gameObject;
    protected Rigidbody rb;
    public float damage;
    public ParticleType onCollitionParticleType;
   
    public void FixedRefresh()
    {
    }

    public virtual void Init(GameObject gameobject, float _damage)
    {
        this.gameObject = gameobject;
        rb = gameobject.GetComponent<Rigidbody>();
        damage = _damage;
    }

    public virtual void Refresh()
    {

    }
    public virtual void OnEnterCollision(Vector3 position)
    {
        
    }
}
