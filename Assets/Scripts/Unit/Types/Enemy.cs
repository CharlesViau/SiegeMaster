using General;
using UnityEngine;

namespace Unit.Types
{
    public class Enemy : MonoBehaviour, IUpdatable, IPoolable, ICreatable<Enemy.Args>
    {
        public EnemyMovement_SO movement_SO;
        public Transform point;

        public class Args : ConstructionArgs
        {
            public Args(Vector3 _spawningPosition) : base(_spawningPosition)
            {
            }
        }

        public void Init()
        {
            movement_SO.Init();
        }

        public void PostInit()
        {

        }

        public void Refresh()
        {
            movement_SO.MoveToPoint(point, 5f);
        }

        public void FixedRefresh()
        {

        }

        public void Pool()
        {

        }

        public void Depool()
        {

        }


        public void Construct(Args constructionArgs)
        {
           
        }
    }
}