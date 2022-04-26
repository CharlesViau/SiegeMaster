using General;
using Managers;
using UnityEngine;

namespace Units.Types
{
    public class Tower : Unit,ICreatable<Tower.Args>
    {

        [HideInInspector]public Transform target;
        public ProjectileType projectiletype;
        public ParticleType towerParticleType;
        public Targeting_SO targeting_SO;
        public float projectileDamage;
        public float towerAttackRange;
        public float attackSpeed;
        public Transform head;
        public Transform barrel;
        public Transform ParticlePosition;


        // for the animation you have to have fire animation and one trigger prameter , the name of trigger must be  "Fire"
        private Animator animator; 


        float timer = 0;

        public override void Init()
        {
            targeting_SO = Instantiate(targeting_SO);
            animator = GetComponent<Animator>();
            base.Init();
        }
        public override void PostInit()
        {
            targeting_SO.Init(this.gameObject, towerAttackRange);
        }
        public override void Refresh()
        {
            CoolDown(attackSpeed);
        }

        protected virtual void GetTarget()
        {
            target = targeting_SO.GetTheTarget();
        }
        public void CoolDown(float _attackSpeed)
        {
            
            timer += Time.deltaTime;
            if (timer > _attackSpeed)
            {
                GetTarget();
                Extrabehavior();
                Fire(target);
                timer = 0;

            }
         
        }
        public virtual void Extrabehavior()
        {

        }
        public  virtual void Fire(Transform target)
        {
            animator.SetTrigger("Fire");
            ParticleSystemManager.Instance.Create(towerParticleType, new ParticleSystemScript.Args(ParticlePosition.position));

        }

        public void Construct(Args constructionArgs)
        {
            transform.position = constructionArgs.spawningPosition;
            targeting_SO.Init(this.gameObject, towerAttackRange);
        }

        public class Args : ConstructionArgs
        {

            public Args(Vector3 _spawningPosition) : base(_spawningPosition)
            {

            }

        }

    }
}
