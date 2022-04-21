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
        public EnemyType enemytype;
        public Targeting_SO targeting_SO;
        public float projectileDamage;
        public float towerAttackRange;
        public float attackSpeed;
        public Transform head;
        public Transform barrel;
        public Transform ParticlePosition;

        float timer = 0;

        public override void Init()
        {
            targeting_SO = Instantiate(targeting_SO);
            base.Init();
        }
        public override void Refresh()
        {
            CoolDown(attackSpeed);
        }

        protected virtual Transform GetTarget()
        {
            return null;
        }
        public void CoolDown(float _attackSpeed)
        {
            timer += Time.time;
            if (timer > _attackSpeed)
            {
                Fire(GetTarget());
                Extrabehavior();
                timer = 0;

            }
        }
        public virtual void Extrabehavior()
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
