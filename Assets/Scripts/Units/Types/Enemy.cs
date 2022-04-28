using General;
using Managers;
using UnityEngine;

namespace Units.Types
{
    public class Enemy : Unit, ICreatable<Enemy.Args>, IHittable
    {
        public EnemyType EnemyType;
        public EnemyMovement_SO movement_SO;
        public Transform[] target;
        int WaypointCounter = 0;
        public bool alive;
        public class Args : ConstructionArgs
        {
            public Args(Vector3 _spawningPosition) : base(_spawningPosition)
            {
            }
        }

        public override void Init()
        {
            base.Init();
            alive = true;
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

            if (Vector3.Distance(transform.position, target[WaypointCounter].position) <= 0.1f)
                WaypointCounter++;
            if (target.Length <= WaypointCounter)
                WaypointCounter = 0;
            movement_SO.MoveToPoint(target[WaypointCounter].position);
        }

        public override void FixedRefresh()
        {

        }

        public override void Pool()
        {
            base.Pool();
            alive = false;
            gameObject.SetActive(false);
        }

        public override void Depool()
        {
            base.Depool();
            alive = true;
            gameObject.SetActive(true);
        }


        public void Construct(Args constructionArgs)
        {
            transform.position = constructionArgs.spawningPosition;
        }

        public void GotShot(float damage)
        {
            GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
           ObjectPool.Instance.Pool(EnemyType, this);
        }

        public override void Move(Vector3 direction)
        {
            base.Move(direction);
        }
    }
}