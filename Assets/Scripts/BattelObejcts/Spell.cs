﻿using General;
using UnityEngine;

namespace BattelObejcts
{
    public class Spell : MonoBehaviour, IUpdatable, IPoolable, ICreatable<Spell.Args>
    {
    

        public void PostInit()
        {
        }

        public void Init()
        {
           

        }

        public void FixedRefresh()
        {
        }

        public void LateRefresh()
        {

        }

        public void Refresh()
        {

        }

        public void Pool()
        {
            gameObject.SetActive(false);
        }

    
        public void Depool()
        {
            gameObject.SetActive(true);
        }
         void ExplosionDamage(Vector3 center, float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            foreach (var hitCollider in hitColliders)
            {
              Debug.Log(hitCollider.gameObject.name);
            }
        }
        public void Construct(Args constructionArgs)
        {
            transform.position = constructionArgs.spawningPosition;
            ExplosionDamage(transform.position, constructionArgs.radius);
        }


        public class Args : ConstructionArgs
        {
            public float radius;

            public Args(Vector3 _spawningPosition,float _radius) : base(_spawningPosition)
            {
                radius = _radius;
            }
        }
    }
}
