using General;
using Units.Interfaces;
using UnityEngine;

namespace BatteObjects
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

        void ExplosionDamage(Vector3 center, float radius, float damage)
        {
            RaycastHit[] ray = Physics.SphereCastAll(center, radius, transform.forward);

            foreach (var hitCollider in ray)
            {
                if (hitCollider.collider.gameObject.TryGetComponent(out IHittable hittable))
                {
                    hittable.GotShot(damage);
                }
            }
        }

        public void Construct(Args constructionArgs)
        {
            transform.position = constructionArgs.spawningPosition;
            ExplosionDamage(transform.position, constructionArgs.Radius, constructionArgs.explosionDamage);
        }


        public class Args : ConstructionArgs
        {
            public readonly float Radius;
            public readonly float explosionDamage;

            public Args(Vector3 spawningPosition, float radius, float explosionDamage) : base(spawningPosition)
            {
                Radius = radius;
                this.explosionDamage = explosionDamage;
            }
        }
    }
}