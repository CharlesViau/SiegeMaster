using System.Numerics;
using Factories;

namespace Unit.Types
{
    public class Enemy : Template.Unit, ICreatable<Enemy.ConstructionArgs>
    {
        public class ConstructionArgs : IArgs
        {
            public ConstructionArgs()
            {
                
            }
            private Vector3 position;
        }

        public void Construct(ConstructionArgs constructionArgs)
        {
            throw new System.NotImplementedException();
        }
    }
}
