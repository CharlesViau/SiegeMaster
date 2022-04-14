using General;
using UnityEngine;

namespace Units.Types
{
    public class Enemy : Unit, ICreatable<Enemy.Args>
    {
        public class Args : ConstructionArgs
        {
            public Vector3 Position;

            public Args(Vector3 position)
            {
                this.Position = position;
            }
        }

        


        public void Construct(Args constructionArgs)
        {
            transform.position = constructionArgs.Position;
        }

        public override void Move(Vector3 direction)
        {
            
        }
    }
}