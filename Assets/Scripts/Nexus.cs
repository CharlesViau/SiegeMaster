using General;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Nexus : MonoBehaviour, IUpdatable, IPoolable, IHittable, ICreatable<Nexus.Args>
{
    Transform enemy;
    float getDamagedRange = 1f;

    public Canvas canvasParent;
    int fullHP;
    public int currentHP;
    Stack<HP> hpStack;

    public void Init()
    {
        enemy = EnemyManager.Instance.GetClosest(transform, getDamagedRange);
        fullHP = currentHP;
        hpStack = new Stack<HP>();
        CreateHp();
    }

    public void PostInit()
    {

    }

    public void Refresh()
    {
        if (enemy != null)
            gameObject.GetComponent<IHittable>().GotShot(1);
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

    public class Args : ConstructionArgs
    {
        public Args(Vector3 _spawningPosition) : base(_spawningPosition)
        {

        }
    }

    void IHittable.GotShot(float damage)
    {
        currentHP -= (int)damage;
        ObjectPool.Instance.Pool(HPType.NexusHp, hpStack.Pop());
    }

    void CreateHp()
    {
        for (int i = 0; i < fullHP; i++)
        {
            HP h = HPManager.Instance.Create(HPType.NexusHp, new HP.Args(Vector3.zero, canvasParent.transform));
            hpStack.Push(h);
        }
    }

    public void LateRefresh()
    {
    }
}
