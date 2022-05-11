using General;
using UnityEngine;

namespace Units.Types.Towers
{
    public class Tower : Unit,ICreatable<Tower.Args>
    {

        [HideInInspector]public Transform target;
        public ProjectileType projectileType;
        public ParticleType towerParticleType;
        public SO.TowerSo.Targeting.TargetingSo targetingSo;
        public float projectileDamage;
        public float towerAttackRange;
        public float attackSpeed;
        public Transform head;
        public Transform barrel;
        public Transform particlePosition;

        protected override Vector3 targetPosition => target.position;

        // Must have fire animation and one trigger parameter , the name of trigger must be  "Fire"
        private static readonly int Fire1 = Animator.StringToHash("Fire");

        private float _timer = 0;

        public override void Init()
        {
            //targetingSo = Instantiate(targetingSo);
            base.Init();
        }
        public override void PostInit()
        {
            targetingSo.Init(this.gameObject, towerAttackRange);
        }
        public override void Refresh()
        {
            CoolDown(attackSpeed);
            
        }
        

        protected virtual void GetTarget()
        {
            target = targetingSo.GetTheTarget();
        }
        public void CoolDown(float _attackSpeed)
        {
            
            _timer += Time.deltaTime;
            if (_timer > _attackSpeed)
            {
                GetTarget();
                ExtraBehaviorBeforeFire();
                if (target)
                {
                    Fire(target);
                }
                
                _timer = 0;

            }
         
        }
        public virtual void ExtraBehaviorBeforeFire()
        {

        }
        public  virtual void Fire(Transform targetTransform)
        {

                Animator.SetTrigger(Fire1);
         
            
            ParticleSystemManager.Instance.Create(towerParticleType, new ParticleSystemScript.Args(particlePosition.position));

        }

        public void Construct(Args constructionArgs)
        {
            transform.position = constructionArgs.spawningPosition;
            targetingSo.Init(this.gameObject, towerAttackRange);
        }

        public class Args : ConstructionArgs
        {

            public Args(Vector3 _spawningPosition) : base(_spawningPosition)
            {

            }

        }

    }
}
