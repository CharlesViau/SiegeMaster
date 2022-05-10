using General;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Nexus : MonoBehaviour, IUpdatable, IPoolable, IHittable, ICreatable<Nexus.Args>
{
    int fullHP;
    public int currentHP;

    public void Init()
    {
        fullHP = currentHP;
    }

    public void PostInit()
    {

    }

    public void Refresh()
    {

        if (currentHP < 1) ;
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
        currentHP -= (int)damage;
    }
}
