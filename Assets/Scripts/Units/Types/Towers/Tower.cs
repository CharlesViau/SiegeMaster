using General;
using Managers;
using UnityEngine;

namespace Units.Types
{
    public class Tower : Unit,ICreatable<Tower.Args>
    {

        public Transform target;
        public ProjectileType projectiletype;
        public ParticleType towerParticleType;
        public EnemyType enemyType;
        public float projectileDamage;
        public float towerAttackRange;
        public float attackSpeed;
        public Transform head;
        public Transform barrel;
        public Transform ParticlePosition;
        public override void Refresh()
        {



        }

        public  virtual void Fire(Transform target)
        {

        }

        public void Construct(Args constructionArgs)
        {
            transform.position = constructionArgs.spawningPosition;
        }

        public class Args : ConstructionArgs
        {

            public Args(Vector3 _spawningPosition) : base(_spawningPosition)
            {

            }

        }

    }
}
