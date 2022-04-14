using General;
using UnityEngine;


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
        Debug.Log("hey");
    }

    public void PostInit()
    {
        movement_SO.Init(gameObject, point, 5f);
    }

    public void Refresh()
    {
        movement_SO.Refresh();
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
        transform.position = constructionArgs.spawningPosition;
    }
}