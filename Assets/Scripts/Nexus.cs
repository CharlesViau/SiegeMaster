using General;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Nexus : MonoBehaviour, IUpdatable, IPoolable, IHittable, ICreatable<Nexus.Args>
{
    private int _fullHp;
    public int currentHp;

    public void Init()
    {
        _fullHp = currentHp;
    }

    public void PostInit()
    {

    }

    public void Refresh()
    {

        //if (currentHP < 1) ;
          //  Debug.Log("Game is over, nexus is destroyed");
    }

    public void FixedRefresh()
    {

    }

    public void LateRefresh()
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

    public class Args : ConstructionArgs
    {
        public Args(Vector3 _spawningPosition) : base(_spawningPosition)
        {

        }
    }
    public void GotShot(float damage)
    {
        currentHp -= (int)damage;
    }
}
