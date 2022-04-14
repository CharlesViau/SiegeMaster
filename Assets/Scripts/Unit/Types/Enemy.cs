using General;
using UnityEngine;


public class Enemy : MonoBehaviour, IUpdatable, IPoolable, ICreatable<Enemy.Args>,IHittable
{
    public EnemyType EnemyType;
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
        Debug.Log("hey");
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
       gameObject.SetActive(false);
    }

    public void Depool()
    {
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