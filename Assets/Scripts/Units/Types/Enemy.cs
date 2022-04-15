using General;
using Managers;
using UnityEngine;

namespace Units.Types
{
    public class Enemy : Unit, ICreatable<Enemy.Args>, IHittable
    {
        public EnemyType EnemyType;
        public EnemyMovement_SO movement_SO;
        public Transform target;

        public class Args : ConstructionArgs
        {
            public Args(Vector3 _spawningPosition) : base(_spawningPosition)
            {
            }
        }

        public override void Init()
        {
            base.Init();
            movement_SO = Instantiate(movement_SO);
            movement_SO.Init(gameObject, target, speed);
            //Debug.Log("hey");
        }

        public override void PostInit()
        {
            base.PostInit();
        }

        public override void Refresh()
        {
            base.Refresh();
            movement_SO.MoveToPoint();
        }

        public override void FixedRefresh()
        {

        }

        public override void Pool()
        {
            base.Pool();
            gameObject.SetActive(false);
        }

        public override void Depool()
        {
            base.Depool();
            gameObject.SetActive(true);
        }


        public void Construct(Args constructionArgs)
        {
            transform.position = constructionArgs.spawningPosition;
        }

        public void GotShot(float damage)
        {
            ObjectPool.Instance.Pool(EnemyType, this);
        }
    }
}