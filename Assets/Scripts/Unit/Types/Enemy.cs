using General;
using UnityEngine;

namespace Unit.Types
{
    public class Enemy : MonoBehaviour, IUpdatable, IPoolable, ICreatable<Enemy.Args>
    {
        public class Args : ConstructionArgs
        {
            public Args(Vector3 _spawningPosition) : base(_spawningPosition)
            {
            }
        }

        public void Init()
        {

        }

        public void PostInit()
        {

        }

        public void Refresh()
        {

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