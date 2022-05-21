using General;
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
        void ExplosionDamage(Vector3 center, float radius,float damage)
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
            ExplosionDamage(transform.position, constructionArgs.radius, constructionArgs.explosionDamage);
        }


        public class Args : ConstructionArgs
        {
            public float radius;
            public float explosionDamage;
            public Args(Vector3 _spawningPosition,float _radius, float _explosionDamage) : base(_spawningPosition)
            {
                radius = _radius;
                explosionDamage = _explosionDamage;
            }
        }
    }
}
