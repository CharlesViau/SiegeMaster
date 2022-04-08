using Factories;
using Managers.Template;
using UnityEngine;

public class Enemy : MonoBehaviour, IUpdaptable, IPoolable, ICreatable<Enemy.Args>
{
    public class Args : ConstructionArgs
    {
        public Vector3 Position;

        public Args(Vector3 position)
        {
            this.Position = position;
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
        transform.position = constructionArgs.Position;
    }
}